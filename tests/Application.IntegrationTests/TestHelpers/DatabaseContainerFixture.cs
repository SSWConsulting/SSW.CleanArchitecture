using Testcontainers.SqlEdge;

namespace Application.IntegrationTests.TestHelpers;

public class DatabaseContainerFixture : IAsyncLifetime
{
    private readonly SqlEdgeContainer? _container;

    public DatabaseContainerFixture()
    {
        ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            _container = new SqlEdgeBuilder()
                .WithName("db")
                .WithPassword("sqledge!Strong")
                .WithAutoRemove(true)
                .Build();
        }
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