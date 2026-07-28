# Use the Factory Pattern to Create Aggregates

- Status: accepted
- Deciders: Daniel Mackay, William Libenberg, Matt Goldman, Luke Parker, Chris Clements
- Tags: ef-core, domain

Technical Story: [SSW.CleanArchitecture | Issues | 111](https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/111)

## Context and Problem Statement

Aggregate roots can be instantiated in a way that satisfies all the decision drivers.  What is the best way to create aggregates?

## Decision Drivers

- Must work well with EF Core
- Must be able to raise domain events on object creation
- Must keep aggregate roots in a valid state at all times

## Considered Options

1. Factory Methods
2. Constructors
3. `required init` properties

## Decision Outcome

Chosen **Option 1. Factory Methods**, because it is the only option that meets all the decision drivers.

See the SSW Rule [Do you encapsulate domain models in Domain-Driven Design?](https://www.ssw.com.au/rules/encapsulate-domain-models), which lists factory methods as one of the ways to keep an aggregate encapsulated.

## Consequences

- Private constructors can be used to ensure that aggregates are always in a valid state
- Need to use `null!` to remove nullable warnings

## Pros and Cons of the Options

### 1. Factory Methods

- ✅ EF Core can still create the aggregate when hydrating data from the DB
- ✅ Domain events can be raised when aggregates are created via the application, but not when they are hydrated from the DB
- ✅ Provides a consistent way to create aggregates
- ✅ Aggregates remain encapsulated

### 2. Constructors

- ❌ EF will incorrectly raise domain events when hydrating data from the DB
- ❌ EF does not allow owned entities to be passed to constructors

### 3. `required init` properties

- ❌ Properties need to be passed to constructors to ensure they are in a valid state on object creation.  Can't use `required init` properties as they then become unmodifiable

## Links

- [Do you encapsulate domain models in Domain-Driven Design?](https://www.ssw.com.au/rules/encapsulate-domain-models)
- [Do you understand the difference between anemic and rich domain models?](https://www.ssw.com.au/rules/anemic-vs-rich-domain-models)
- [Do you know when to use value objects?](https://www.ssw.com.au/rules/when-to-use-value-objects)
