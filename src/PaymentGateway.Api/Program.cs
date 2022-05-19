using PaymentGateway.Api.Extensions;
using PaymentGateway.Api.Middleware;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.SetupSwagger();
builder.SetupAuth();
builder.AddMediatRApi();
builder.AddAutoMapperApi();
builder.AddRepository();
builder.AddGatewayClients();
builder.AddVersioningAndLogs();

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

app.UseMiddleware<ErrorHandlingMiddleware>(app.Environment.IsDevelopment());
app.SetupMigrationAndSeed();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAuth();
app.Run();
