using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.WebApi;
using System.Data.Common;
using TUnit.Core.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WebApi.IntegrationTests.Common.Infrastructure.Web;

/// <summary>
/// Host builder (services, DI and configuration) for integration tests
/// </summary>
public class WebApiTestFactory : WebApplicationFactory<IWebApiMarker>
{
    private readonly DbConnection _dbConnection;

    public WebApiTestFactory(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Redirect application logging to test output
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            // TODO: This doesn't seem to log everything. Need to investigate further
            logging.AddProvider(new TUnitLoggerProvider());
        });

        builder.UseSetting("ConnectionStrings:clean-architecture", _dbConnection.ConnectionString);
    }
}

public class TUnitLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new TUnitDefaultLogger();
    }

    public void Dispose()
    {
        // Dispose resources if any
    }
}

public class TUnitDefaultLogger : ILogger
{
    private readonly TUnit.Core.Logging.DefaultLogger _logger = new();

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _logger.LogInformation(formatter(state, exception));
    }
}