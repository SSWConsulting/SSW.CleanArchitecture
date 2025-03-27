namespace WebApi.IntegrationTests.Common;

[CollectionDefinition(Name)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.

    public const string Name = nameof(TestingDatabaseFixtureCollection);
}