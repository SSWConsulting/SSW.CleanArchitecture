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
            // logging.ClearProviders();
            // DM: Fix up
            // logging.AddProvider(new TestLoggerProvider());
        });

        builder.UseSetting("ConnectionStrings:clean-architecture", _dbConnection.ConnectionString);
    }
}

// public class TestLoggerProvider : ILoggerProvider
// {
//     public ILogger CreateLogger(string categoryName)
//     {
//         return new TestLogger();
//     }
//
//     public void Dispose()
//     {
//         // Dispose resources if any
//     }
// }
//
// public class TestLogger : ILogger
// {
//     public IDisposable? BeginScope<TState>(TState state) where TState : notnull
//     {
//         return null;
//     }
//
//     public bool IsEnabled(LogLevel logLevel)
//     {
//         return true;
//     }
//
//     public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
//     {
//         var msg = formatter(state, exception);
//         Console.WriteLine(msg);
//
//         var logger = TestContext.Current?.GetDefaultLogger();
//         if (logger == null)
//             return;
//
//         logger.LogInformation(formatter(state, exception));
//     }
// }