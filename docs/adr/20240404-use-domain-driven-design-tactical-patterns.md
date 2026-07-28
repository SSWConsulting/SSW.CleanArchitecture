# Use Domain-Driven Design Tactical Patterns

- Status: Accepted
- Deciders: Daniel Mackay, Matt Goldman, Matt Wicks, Luke Parker, Chris Clement
- Date: 2024-04-04
- Tags: ddd

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/283


## Context and Problem Statement

The current Clean Architecture framework relies on an anemic domain model, which simplifies initial development but increasingly hampers our ability to handle the complex interactions and business logic inherent in our domain. By incorporating Domain-Driven Design (DDD), projects with non-trivial logic can better accommodate complex workflows and business rule integrations without compromising maintainability or scalability.

We would like to default to using DDD in the template and provide a good example of building applications in that manner.


## Considered Options

1. Anemic Domain Model
2. Rich Domain Model with DDD

## Decision Outcome

Chosen option: "Option 2 - Rich Domain Model with DDD", because it helps set developers up for success when building complex applications.  It's easier to go from a rich domain model to an anemic domain model than the other way around.

See the SSW Rule [Do you understand the difference between anemic and rich domain models?](https://www.ssw.com.au/rules/anemic-vs-rich-domain-models) for the wider guidance behind this decision.

### Consequences <!-- optional -->

- Need to create a new Domain model to show the usefulness of DDD.  This will require most layers to be rebuilt.

## Pros and Cons of the Options <!-- optional -->

### Option 1 - Anemic Domain Model

- ✅ Simplier for trivial applications
- ❌ Difficult to upgrade to use DDD patterns

### Option 2 - Rich Domain Model with DDD

- ✅ Easy to migrate to an anemic domain model if needed
- ✅ More flexible for complex applications
- ❌ Overkill for trivial applications
- ❌ More complex to understand

## Links

- [Do you understand the difference between anemic and rich domain models?](https://www.ssw.com.au/rules/anemic-vs-rich-domain-models)
- [Do you encapsulate domain models in Domain-Driven Design?](https://www.ssw.com.au/rules/encapsulate-domain-models)
- [Do you use ubiquitous language?](https://www.ssw.com.au/rules/ubiquitous-language)
- [Clean Architecture - Do you know the main principles?](https://www.ssw.com.au/rules/clean-architecture)
