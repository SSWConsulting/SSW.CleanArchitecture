# Use Vogen to simplify strongly typed IDs

- Status: approved
- Deciders: Daniel Mackay
- Date: 2024-11-19
- Tags: ddd, vogen, strongly-typed-ids

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/364

## Context and Problem Statement

Strongly typed IDs are a great way to combat primitive obsession. However, they can be a bit of a pain to set up especially with EF Core. We want to make it easier to use strongly typed IDs or any simple value objects in our projects.

## Decision Drivers <!-- optional -->

- Simplify usage of strongly typed IDs

## Considered Options

1. Manual Approach
2. Vogen

## Decision Outcome

Chosen option: "Option 2 - Vogen", because configuration, validation, and usage of strongly typed IDs is much simpler (espeicially from the EF Core point of view).

## Pros and Cons of the Options <!-- optional -->

### Option 1 - Manual Approach

- ✅ Avoid additional dependencies
- ❌ Extra EF Core boilerplate needed
- ❌ Not easy to add value object validation with current record-based approach

### Option 2 - Vogen

- ✅ Simple to create and use strongly typed IDs
- ✅ Easy to add value object validation
- ✅ Easy to add EF Core configuration
- ✅ Can be used for any simple value object
- ❌ Extra dependency added

## Links

- https://stevedunn.github.io/Vogen/overview.html
- https://github.com/SteveDunn/Vogen
