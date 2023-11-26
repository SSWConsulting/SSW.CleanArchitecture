using Testcontainers.SqlEdge;

namespace WebApi.IntegrationTests.Common.Fixtures;

/// <summary>
/// Wraper for SQL edge container
/// </summary>
public class DatabaseContainer
{
    private readonly SqlEdgeContainer? _container = new SqlEdgeBuilder()
        .WithName("CleanArchitecture-IntegrationTests-DbContainer")
        .WithPassword("sqledge!Strong")
        .WithAutoRemove(true)
        .Build();

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        if (_container != null)
        {
            await _container.StartAsync();
            ConnectionString = _container.GetConnectionString();
        }
    }

    public Task DisposeAsync() => _container?.StopAsync() ?? Task.CompletedTask;
}