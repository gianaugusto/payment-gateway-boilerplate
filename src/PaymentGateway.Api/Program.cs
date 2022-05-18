
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

using PaymentGateway.Api.Extensions;
using PaymentGateway.Application.Features.PaymentProcessor.Domain.Interfaces;
using PaymentGateway.Data.SQL.Context;
using PaymentGateway.Data.SQL.Repositories;
using PaymentGateway.Data.SQL.Tools;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SetupSwagger();
builder.Services.SetupAuth(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMediatRApi();
builder.Services.AddMapper();

builder.Services
    .AddDbContext<ServiceContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddScoped<DbContext, ServiceContext>()
    .AddTransient<IPaymentRepository, PaymentRepository>();

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<ServiceContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments - V1");

    });
}

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
    Console.Write(ex);
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuth();
app.Run();
