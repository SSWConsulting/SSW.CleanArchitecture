using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MigrationService.Initializers;
using Respawn;
using SSW.CleanArchitecture.WebApi;
using TUnit.Core.Interfaces;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Initializes and resets the database before and after each test
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class WebApplicationFactory: WebApplicationFactory<IWebApiMarker>, IAsyncInitializer
{
    private string ConnectionString => Database.ConnectionString!;

    private Respawner _checkpoint = default!;

    public IServiceScopeFactory ScopeFactory { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        // Initialize DB Container
        await Database.InitializeAsync();
        ScopeFactory = Services.GetRequiredService<IServiceScopeFactory>();

        // Create and seed database
        using var scope = ScopeFactory.CreateScope();
        var warehouseInitializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await warehouseInitializer.EnsureDatabaseAsync(default);
        await warehouseInitializer.CreateSchemaAsync(true, default);

        // NOTE: If there are any tables you want to skip being reset, they can be configured here
        _checkpoint = await Respawner.CreateAsync(ConnectionString);
    }

    public override async ValueTask DisposeAsync()
    {
        await Database.DisposeAsync();
        await base.DisposeAsync();
    }

    public async Task ResetState()
    {
        await _checkpoint.ResetAsync(ConnectionString);
    }

    public DatabaseContainer Database { get; } = new();

    // public ITestOutputHelper Output { get; set; } = null!;

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
            // TODO: Fix up logging
            // x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(Output));
        });

        // Override default DB registration to use out Test Container instead
        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<ApplicationDbContextInitializer>();
        });

        builder.UseSetting("ConnectionStrings:clean-architecture", Database.ConnectionString);
    }
}