using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.WebApi;
using System.Data.Common;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Leverages default host setup to allow for integration testing
/// </summary>
public class CustomWebApplicationFactory: WebApplicationFactory<IWebApiMarker>
{
    private readonly DbConnection _dbConnection;

    public CustomWebApplicationFactory(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Redirect application logging to test output
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders();
            x.AddFilter(level => level >= LogLevel.Information);
            // TODO: Fix up logging
            // x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(Output));
        });

        builder.UseSetting("ConnectionStrings:clean-architecture", _dbConnection.ConnectionString);
    }
}