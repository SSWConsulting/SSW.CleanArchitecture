# Use Aggregate Specification Classes with Factory Methods

- Status: accepted
- Deciders: Daniel Mackay, Anton Polkanov
- Date: 2026-02-21
- Tags: domain, specifications

## Context and Problem Statement

The previous approach created a separate class file per specification (e.g., `TeamByIdSpec`, `HeroByIdSpec`). As the number of specifications grows, this leads to file proliferation and poor discoverability — developers must know the exact class name to find a specification, and there is no single place to browse all available specifications for a given aggregate.

## Decision Drivers

- Improve discoverability of specifications per aggregate
- Reduce the number of files in the Domain layer
- Keep specifications co-located with their aggregate

## Considered Options

1. One class per specification (previous approach)
2. Single class per aggregate with static factory methods

## Decision Outcome

Chosen option: **Option 2 - Single class per aggregate with static factory methods**, because it groups all specifications for an aggregate in one discoverable location and reduces file count without sacrificing clarity.

The class itself extends `SingleResultSpecification<T>` and static factory methods configure instances via `spec.Query`, following a consistent naming convention (`{Aggregate}Spec`).

```csharp
public sealed class HeroSpec : SingleResultSpecification<Hero>
{
    public static HeroSpec ById(HeroId heroId)
    {
        var spec = new HeroSpec();
        spec.Query.Where(h => h.Id == heroId);
        return spec;
    }
}
```

Usage:

```csharp
dbContext.Heroes.WithSpecification(HeroSpec.ById(heroId)).FirstOrDefault();
```

### Consequences

- ✅ All specifications for an aggregate live in one file (`HeroSpec.cs`, `TeamSpec.cs`)
- ✅ Intention-revealing factory method names (`HeroSpec.ById(...)`)
- ✅ IntelliSense on `HeroSpec.` surfaces all available specifications immediately
- ✅ No inner classes or extra indirection needed
- ❌ Slightly more boilerplate per specification (factory method vs constructor-only class)

## Pros and Cons of the Options

### Option 1 - One class per specification

- ✅ Minimal boilerplate for a single specification
- ❌ File proliferation as aggregate queries grow
- ❌ No single place to discover all specifications for an aggregate
- ❌ Class names must be memorised to find the right specification

### Option 2 - Single class per aggregate with static factory methods

- ✅ All specifications browsable via IntelliSense on the aggregate's spec class
- ✅ Fewer files in the Domain layer
- ✅ Consistent naming convention (`{Aggregate}Spec`)
- ✅ No inner class boilerplate
- ❌ Slightly more verbose per specification entry
