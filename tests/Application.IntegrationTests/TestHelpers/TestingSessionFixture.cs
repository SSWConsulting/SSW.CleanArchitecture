namespace Application.IntegrationTests.TestHelpers;

public class TestingSessionFixture : IDisposable
{
    public TestingSessionFixture(TestingDatabaseFixture databaseFixture)
    {
        databaseFixture.ResetState().GetAwaiter().GetResult();
    }

    public void Dispose()
    {
    }
}