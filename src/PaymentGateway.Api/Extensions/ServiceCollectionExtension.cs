namespace PaymentGateway.Api.Extensions
{
    using Hangfire;

    using IdentityModel.Client;

    using MediatR;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.OpenApi.Models;

    using PaymentGateway.Api.Handler;
    using PaymentGateway.Application.Features.PaymentProcessor.Domain.Interfaces;
    using PaymentGateway.Application.Features.PaymentProcessor.Gateways.Clients;
    using PaymentGateway.Application.Features.PaymentProcessor.Gateways.Interfaces;
    using PaymentGateway.CrossCutting;
    using PaymentGateway.Data.SQL.Context;
    using PaymentGateway.Data.SQL.Repositories;
    using PaymentGateway.Data.SQL.Tools;

    using Polly;
    using Polly.Extensions.Http;

    using Serilog;

    public static class ServiceCollectionExtension
    {
       public static void AddVersioningAndLogs(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(option =>
            {
                option.DefaultApiVersion = new ApiVersion(1, 0);
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.Services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console()
                .ReadFrom.Configuration(ctx.Configuration));
        }

        public static void AddMediatRApi(this WebApplicationBuilder builder)
        {
            const string applicationAssemblyName = "PaymentGateway.Application";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            builder.Services.AddMediatR(assembly);
        }

        public static void UseAuth(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
            });
        }

        public static void SetupMigrationAndSeed(this WebApplication app)
        {
            try
            {
                Task.Run(() =>
                {
                    using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
                    var context = serviceScope?.ServiceProvider.GetRequiredService<ServiceContext>();

                    if (context == null || !context.Database.EnsureCreated()) return;

                    context.Database.Migrate();
                    new Seed(context).SeedServiceData();
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Setup migration or seed fail");
            }
        }

        public static void SetupAuth(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
                {
                    c.Authority = $"https://{builder.Configuration["Auth:Domain"]}";
                    c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidAudience = builder.Configuration["Auth:Audience"],
                        ValidIssuer = $"{builder.Configuration["Auth:Domain"]}"
                    };
                });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            builder.Services.AddAuthorization(o =>
            {
                o.AddPolicy("pay:read-write", p => p.
                    RequireAuthenticatedUser().
                    RequireClaim("scope", "pay:read-write"));
            });
        }

        public static void SetupSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Gateway API - V1", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void AddAutoMapperApi(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(PaymentGateway.Application.Features.PaymentProcessor.MappingProfile));
        }

        public static void AddRepository(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<ServiceContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
                .AddScoped<DbContext, ServiceContext>()
                .AddTransient<IPaymentRepository, PaymentRepository>();

            builder.Services
                .AddHealthChecks()
                .AddDbContextCheck<ServiceContext>();
        }

        public static void AddQueueProcessor(this WebApplicationBuilder builder)
        {
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddHangfireServer();
        }

        public static void AddGatewayClients(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(new ClientCredentialsTokenRequest
            {
                Address = builder.Configuration["Services:ActivoApi:OAuthSettings:Url"],
                ClientId = builder.Configuration["Services:ActivoApi:OAuthSettings:ClientId"],
                ClientSecret = builder.Configuration["Services:ActivoApi:OAuthSettings:ClientSecret"],
                Scope = builder.Configuration["Services:ActivoApi:OAuthSettings:Scopes"],
                Parameters =
                {
                    { "audience", builder.Configuration["Services:ActivoApi:OAuthSettings:audience"]}
                }
            });
            
            builder.Services.AddHttpClient<IBankApiClient, ActivoApiClient>(client =>
                {
                    client.BaseAddress = new Uri(builder.Configuration["Services:ActivoApi:Url"]);
                })
                //.AddHttpMessageHandler<BearerTokenHandler>() TODO : complete auth before include this handler
                .AddPolicyHandler(
                    GetRetryPolicy(
                        builder.Configuration.GetValue<int>("Services:ActivoApi:ResilienceSettings:RetryTimes"),
                        builder.Configuration.GetValue<int>("Services:ActivoApi:ResilienceSettings:Retry:DelayBetweenRetriesInMs"))
                    );
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount, int delayBetweenRetriesInMs)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromMilliseconds(delayBetweenRetriesInMs));
        }
    }
}
