using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Database;
using SSW.CleanArchitecture.Infrastructure.Persistence;
using SSW.CleanArchitecture.WebApi;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Host builder (services, DI and configuration) for integration tests
/// </summary>
public class WebApiTestFactory : WebApplicationFactory<IWebApiMarker>
{
    public DatabaseContainer Database { get; } = new();

    public ITestOutputHelper Output { get; set; } = null!;

    // NOTE: If you need an authenticated client, create a similar method that performance the authentication,
    // adds the appropriate headers, and returns the authenticated client
    // For an example of this see https://github.com/SSWConsulting/Northwind365
    public Lazy<HttpClient> AnonymousClient => new(CreateClient());

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Redirect application logging to test output
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders();
            x.AddFilter(level => level >= LogLevel.Information);
            x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(Output));
        });

        // Override default DB registration to use out Test Container instead
        builder.ConfigureTestServices(services =>
        {
            services.ReplaceDbContext<ApplicationDbContext>(Database);
            services.AddScoped<ApplicationDbContextInitializer>();
        });
    }
}