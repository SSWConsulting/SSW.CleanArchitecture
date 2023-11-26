using Microsoft.Extensions.DependencyInjection;
using Respawn;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Initializes and resets the database before and after each test
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class TestingDatabaseFixture : IAsyncLifetime
{
    private string ConnectionString => Factory.Database.ConnectionString!;

    private Respawner _checkpoint = default!;

    public IServiceScopeFactory ScopeFactory { get; private set; } = default!;

    public readonly WebApiTestFactory Factory = new();

    public async Task InitializeAsync()
    {
        await Factory.Database.InitializeAsync();
        ScopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();

        // NOTE: If there are any tables you want to skip being reset, they can be configured here
        _checkpoint = await Respawner.CreateAsync(ConnectionString);
    }

    public async Task DisposeAsync()
    {
        await Factory.Database.DisposeAsync();
    }

    public async Task ResetState()
    {
        await _checkpoint.ResetAsync(ConnectionString);
    }
}

[CollectionDefinition(Name)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.

    public const string Name = nameof(TestingDatabaseFixtureCollection);
}