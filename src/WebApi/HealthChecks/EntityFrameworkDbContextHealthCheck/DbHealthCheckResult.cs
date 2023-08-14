namespace SSW.CleanArchitecture.WebApi.HealthChecks.EntityFrameworkDbContextHealthCheck;

public sealed class DbHealthCheckResult
{
    public bool Success { get; set; }
    public Exception? Exception { get; set; }
    public string? Message { get; set; } = string.Empty;
}
