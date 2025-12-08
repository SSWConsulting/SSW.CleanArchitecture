# SSW Clean Architecture - Copilot Instructions

This is a .NET 9 Clean Architecture template using Domain-Driven Design (DDD) tactical patterns, CQRS with MediatR, and .NET Aspire for orchestration.

## Architecture Overview

```
src/
├── Domain/          # Entities, Value Objects, Aggregates, Domain Events, Specifications
├── Application/     # Use Cases (Commands/Queries), Interfaces, Validators
├── Infrastructure/  # EF Core, External Services, Persistence
├── WebApi/          # Minimal API Endpoints, Extensions
tools/
├── AppHost/         # .NET Aspire orchestration
├── MigrationService/ # EF Core migrations runner
```

**Dependency Flow**: WebApi → Application → Domain ← Infrastructure

## Running the Solution

```bash
cd tools/AppHost && dotnet run
```

Aspire handles database provisioning, migrations, and seeding automatically. API available at `https://localhost:7255/scalar/v1`.

## Key Patterns

### Commands (Create/Update/Delete)
Location: `src/Application/UseCases/{Feature}/Commands/{CommandName}/`

```csharp
public sealed record CreateHeroCommand(string Name, string Alias) : IRequest<ErrorOr<Guid>>;

internal sealed class CreateHeroCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<CreateHeroCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateHeroCommand request, CancellationToken ct)
    {
        var hero = Hero.Create(request.Name, request.Alias);
        await dbContext.Heroes.AddAsync(hero, ct);
        await dbContext.SaveChangesAsync(ct);
        return hero.Id.Value;
    }
}

internal sealed class CreateHeroCommandValidator : AbstractValidator<CreateHeroCommand>
{
    public CreateHeroCommandValidator() => RuleFor(v => v.Name).NotEmpty();
}
```

### Queries (Read)
Location: `src/Application/UseCases/{Feature}/Queries/{QueryName}/`

```csharp
public record GetAllHeroesQuery : IRequest<IReadOnlyList<HeroDto>>;
public record HeroDto(Guid Id, string Name, string Alias, int PowerLevel);

internal sealed class GetAllHeroesQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetAllHeroesQuery, IReadOnlyList<HeroDto>>
{
    public async Task<IReadOnlyList<HeroDto>> Handle(GetAllHeroesQuery request, CancellationToken ct)
        => await dbContext.Heroes.Select(h => new HeroDto(h.Id.Value, h.Name, h.Alias, h.PowerLevel)).ToListAsync(ct);
}
```

### Domain Entities with Strongly-Typed IDs (Vogen)
Location: `src/Domain/{Feature}/`

```csharp
[ValueObject<Guid>]
public readonly partial struct HeroId;

public class Hero : AggregateRoot<HeroId>
{
    private Hero() { }  // EF Core constructor
    
    public static Hero Create(string name, string alias)
        => new() { Id = HeroId.From(Guid.CreateVersion7()), Name = name, Alias = alias };
}
```

### Domain Events
Raise events from aggregates to trigger side effects. Events are dispatched after `SaveChangesAsync()`.

**Define event** in `src/Domain/{Feature}/`:
```csharp
public sealed record PowerLevelUpdatedEvent(Hero Hero) : IDomainEvent;
```

**Raise from aggregate**:
```csharp
public void UpdatePowers(IEnumerable<Power> powers)
{
    _powers.Clear();
    foreach (var power in powers) AddPower(power);
    AddDomainEvent(new PowerLevelUpdatedEvent(this));  // Raise event
}
```

**Handle event** in `src/Application/UseCases/{Feature}/EventHandlers/`:
```csharp
internal sealed class PowerLevelUpdatedEventHandler : INotificationHandler<PowerLevelUpdatedEvent>
{
    public Task Handle(PowerLevelUpdatedEvent notification, CancellationToken ct)
    {
        // Side effects: send emails, update read models, integrate with external systems
        return Task.CompletedTask;
    }
}
```

### Minimal API Endpoints
Location: `src/WebApi/Endpoints/`

```csharp
public static void MapHeroEndpoints(this WebApplication app)
{
    var group = app.MapApiGroup("heroes");
    
    group.MapPost("/", async (ISender sender, CreateHeroCommand command, CancellationToken ct) =>
    {
        var result = await sender.Send(command, ct);
        return result.Match(_ => TypedResults.Created(), CustomResult.Problem);
    })
    .WithName("CreateHero")
    .ProducesPost();  // Use extension methods for consistent status codes
}
```

### Result Pattern (ErrorOr)
Use `ErrorOr<T>` for commands, not exceptions. Handle with `.Match()`:
```csharp
result.Match(success => TypedResults.Ok(success), CustomResult.Problem);
```

## Testing

### Integration Tests
Location: `tests/WebApi.IntegrationTests/Endpoints/{Feature}/`

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

Uses TestContainers + Respawn for real database testing.

### Unit Tests
Location: `tests/Domain.UnitTests/` - Test domain logic without EF Core mocking (Specifications pattern).

### Architecture Tests  
Location: `tests/Architecture.Tests/` - NetArchTest enforces layer dependencies.

## EF Migrations

```bash
# Add migration
dotnet ef migrations add YourMigration --project ./src/Infrastructure --startup-project ./src/WebApi --output-dir ./Persistence/Migrations

# Migrations apply automatically via Aspire MigrationService
```

## Conventions

- **No AutoMapper** - Use manual mapping with `Select()` projections
- **Strongly-typed IDs** - All entities use Vogen `[ValueObject<Guid>]`
- **Factory methods** - Create aggregates via static `Create()` methods, not constructors
- **Specifications** - Query logic in Domain layer (e.g., `TeamByIdSpec`)
- **FluentValidation** - Validators in same folder as Command/Query
- **Awesome Assertions** - Use `Should()` syntax in tests
- **Code generation** - Reference existing code in `src/Application/UseCases/Heroes/` as patterns

## ADRs
Architectural decisions documented in `docs/adr/`. Key decisions:
- Results pattern over exceptions
- Vogen for strongly-typed IDs
- Manual mapping over AutoMapper
- Specifications in Domain layer
