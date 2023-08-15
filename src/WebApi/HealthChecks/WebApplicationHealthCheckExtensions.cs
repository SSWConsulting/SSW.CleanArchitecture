using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

public static class WebApplicationHealthCheckExtensions
{
    public static WebApplication UseHealthChecks(this WebApplication app)
    {
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

        return app;
    }
}