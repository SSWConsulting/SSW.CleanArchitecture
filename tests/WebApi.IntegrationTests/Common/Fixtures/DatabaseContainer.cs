using Polly;
using Testcontainers.MsSql;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Wraper for SQL edge container
/// </summary>
public class DatabaseContainer : IAsyncDisposable
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
        .WithName($"CleanArchitecture-IntegrationTests-{Guid.CreateVersion7()}")
        .WithPassword("Password123")
        .WithPortBinding(1433, true)
        .WithAutoRemove(true)
        .Build();

    private const int MaxRetries = 5;

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        await StartWithRetry();
        ConnectionString = _container.GetConnectionString();
    }

    private async Task StartWithRetry()
    {
        // NOTE: For some reason the container sometimes fails to start up.  Add in a retry to protect against this
        var policy = Policy.Handle<InvalidOperationException>()
            .WaitAndRetryAsync(MaxRetries, _ => TimeSpan.FromSeconds(5));

        await policy.ExecuteAsync(async () => { await _container.StartAsync(); });
    }

    public async ValueTask DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}