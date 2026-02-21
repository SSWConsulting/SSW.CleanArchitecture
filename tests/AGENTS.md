# SSW Clean Architecture - Testing Patterns

This file covers all testing patterns across integration, unit, and architecture test projects.

## Shared Conventions

- **Awesome Assertions** - Use `Should()` syntax for all assertions

## Integration Tests

Location: `tests/WebApi.IntegrationTests/Endpoints/{Feature}/`

Uses TestContainers (real database) + Respawn (database reset between tests).

```csharp
public class CreateHeroCommandTests(TestingDatabaseFixture fixture) : IntegrationTestBase(fixture)
{
    [Fact]
    public async Task Command_ShouldCreateHero()
    {
        var cmd = new CreateHeroCommand("Clark Kent", "Superman", []);
        var client = GetAnonymousClient();

        var result = await client.PostAsJsonAsync("/api/heroes", cmd, CancellationToken);

        result.StatusCode.Should().Be(HttpStatusCode.Created);
        var hero = await GetQueryable<Hero>().FirstAsync(CancellationToken);
        hero.Name.Should().Be(cmd.Name);
    }
}
```

Key helpers available from `IntegrationTestBase`:
- `GetAnonymousClient()` - unauthenticated `HttpClient`
- `GetQueryable<T>()` - direct EF Core access to verify database state

## Unit Tests

Location: `tests/Domain.UnitTests/`

Test domain logic directly — no EF Core, no mocking of infrastructure. Focus on:
- Aggregate behaviour and invariants
- Specifications pattern (e.g., `TeamByIdSpec`)
- Domain event raising

## Architecture Tests

Location: `tests/Architecture.Tests/`

Uses NetArchTest to enforce layer dependency rules. Ensures no illegal references between layers (e.g., Domain must not reference Application or Infrastructure).
