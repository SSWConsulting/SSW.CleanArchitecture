namespace Application.IntegrationTests.TestHelpers;

[Collection(TestingDatabaseFixture.DatabaseCollectionDefinition)]
public abstract class IntegrationTestBase : IAsyncLifetime, IClassFixture<TestingSessionFixture>
{
    protected TestingDatabaseFixture Fixture { get; }

    public IntegrationTestBase(TestingDatabaseFixture fixture)
    {
        Fixture = fixture;
    }

    public async Task InitializeAsync()
    {
        await Fixture.ResetState();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}