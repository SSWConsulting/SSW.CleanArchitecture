using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using SSW.CleanArchitecture.Application;
using SSW.CleanArchitecture.Infrastructure;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.WebApi;
using SSW.CleanArchitecture.WebApi.Features;
using SSW.CleanArchitecture.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initializer.InitializeAsync();
    await initializer.SeedAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Basic Healthy/Degraded/Unhealthy result
app.UseHealthChecks("/health");

// Detailed Report about each check
// TODO: Because of the detailed information, this endpoint should be secured behind
// an Authorization Policy (.RequireAuthorization()), or a specific secured port through firewall rules
app.UseHealthChecks("/health-report", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi3(settings => settings.DocumentPath = "/api/specification.json");

app.UseRouting();

app.UseExceptionFilter();

app.MapTodoItemEndpoints();

app.Run();

public partial class Program { }