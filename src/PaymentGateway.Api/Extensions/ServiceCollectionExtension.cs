namespace PaymentGateway.Api.Extensions
{
    using MediatR;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.OpenApi.Models;

    using PaymentGateway.CrossCutting;

    public static class ServiceCollectionExtension
    {
        public static void AddMediatRApi(this IServiceCollection services)
        {
            const string applicationAssemblyName = "PaymentGateway.Application";
            var assembly = AppDomain.CurrentDomain.Load(applicationAssemblyName);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            services.AddMediatR(assembly);
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

        public static void SetupAuth(this IServiceCollection services, IConfigurationRoot configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
                {
                    c.Authority = $"https://{configuration["Auth:Domain"]}";
                    c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidAudience = configuration["Auth:Audience"],
                        ValidIssuer = $"{configuration["Auth:Domain"]}"
                    };
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .Services.AddAuthorization(o =>
                {
                    o.AddPolicy("pay:read-write", p => p.
                        RequireAuthenticatedUser().
                        RequireClaim("scope", "pay:read-write"));
                });

        }

        public static void SetupSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
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

        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentGateway.Application.Features.PaymentProcessor.MappingProfile));
        }
    }
}
