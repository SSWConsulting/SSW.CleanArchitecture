using Microsoft.Extensions.DependencyInjection;

namespace WebApi.IntegrationTests.Common.Fixtures;

public static class Testing
{
    private static SqlServerTestDatabase _database = new();
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;

    [Before(Assembly)]
    public static async Task GlobalSetup()
    {
        await _database.InitializeAsync();
        _factory = new CustomWebApplicationFactory(_database.GetConnection());
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [BeforeEvery(Test)]
    public static async Task TestSetup(TestContext context)
    {
        // Nothing to do yet
    }

    [AfterEvery(Test)]
    public static async Task TestCleanUp(TestContext context)
    {
        await _database.ResetAsync();
    }

    [After(Assembly)]
    public static async Task GlobalCleanUp()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }

    // NOTE: If you need an authenticated client, create a similar method that performance the authentication,
    // adds the appropriate headers, and returns the authenticated client
    // For an example of this see https://github.com/SSWConsulting/Northwind365
    public static Lazy<HttpClient> AnonymousClient => new(_factory.CreateClient());

    public static IServiceScope CreateScope() => _scopeFactory.CreateScope();
}