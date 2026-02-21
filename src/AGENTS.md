# SSW Clean Architecture - Implementation Patterns

This file covers conventions, migrations, and all coding patterns for the Domain, Application, Infrastructure, and WebApi layers.

## Conventions

- **No AutoMapper** - Use manual mapping with `Select()` projections
- **Strongly-typed IDs** - All entities use Vogen `[ValueObject<Guid>]`
- **Factory methods** - Create aggregates via static `Create()` methods, not constructors
- **Specifications** - Query logic in Domain layer (e.g., `TeamByIdSpec`)
- **FluentValidation** - Validators in same folder as Command/Query
- **Awesome Assertions** - Use `Should()` syntax in tests
- **Code generation** - Reference existing code in `src/Application/UseCases/Heroes/` as patterns

## EF Migrations

```bash
# Add migration
dotnet ef migrations add YourMigration --project ./src/Infrastructure --startup-project ./src/WebApi --output-dir ./Persistence/Migrations

# Migrations apply automatically via Aspire MigrationService
```

## Commands (Create/Update/Delete)

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

## Queries (Read)

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

## Domain Entities with Strongly-Typed IDs (Vogen)

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

## Domain Events

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

## Minimal API Endpoints

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

## Result Pattern (ErrorOr)

Use `ErrorOr<T>` for commands, not exceptions. Handle with `.Match()` at the HTTP layer:

```csharp
result.Match(success => TypedResults.Ok(success), CustomResult.Problem);
```
