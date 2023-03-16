using Testcontainers.SqlEdge;

namespace Application.IntegrationTests.TestHelpers;

public class DatabaseContainer
{
    private readonly SqlEdgeContainer? _container;

    public DatabaseContainer()
    {
        _container = new SqlEdgeBuilder()
            .WithName("db")
            .WithPassword("sqledge!Strong")
            .WithAutoRemove(true)
            .Build();
    }

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