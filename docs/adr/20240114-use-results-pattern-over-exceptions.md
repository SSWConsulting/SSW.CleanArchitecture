# Use Results Pattern Over Exceptions

- Status: proposed
- Deciders: {{ DECIDERS }} <!-- optional: list everyone involved in the decision -->
- Date: 2024-01-14
- Tags: {{ TAGS }} <!-- optional: space and/or comma-separated list of tags -->

Technical Story: {{ TECH STORY }} <!-- optional: description | ticket/issue URL -->

## Context and Problem Statement

We currently use exceptions for validation errors and subsequently flow control. This is not ideal as exceptions are expensive and should be used for exceptional circumstances only.

According to David Fowler exceptions are **very expensive** in ASP.NET Core.

## Decision Drivers <!-- optional -->

- {{ DRIVER 1 }} <!-- e.g., a force, facing concern, … -->
- {{ DRIVER 2 }} <!-- e.g., a force, facing concern, … -->
- … <!-- numbers of drivers can vary -->

## Considered Options

1. {{ OPTION 1 }}
2. {{ OPTION 2 }}
3. {{ OPTION 3 }}

## Decision Outcome

Chosen option: "{{ OPTION 1 }}", because {{ JUSTIFICATION }} <!-- e.g., only option, which meets k.o. criterion decision driver | which resolves force force | … | comes out best (see below) -->.

### Consequences <!-- optional -->

- ✅ {{ POSITIVE CONSEQUENCE 1 }}
- ❌ {{ NEGATIVE CONSEQUENCE 1 }}

## Pros and Cons of the Options <!-- optional -->

### Option 1 - Use Exceptions for Validation Flow Control

As per the current implementation.

- ✅ {{ ARGUMENT A }}
- ❌ {{ ARGUMENT B }}

### Option 2 - Use Result Pattern for Validation and Flow Control

Use the result pattern. This could be implemented via:

- [Ardalis.Result](https://github.com/ardalis/Result)
- [Fluent Results](https://github.com/altmann/FluentResults)
- [OneOf](https://github.com/mcintyre321/OneOf)

- ✅ {{ ARGUMENT A }}
- ❌ {{ ARGUMENT B }}

## Links <!-- optional -->

- https://ardalis.com/avoid-using-exceptions-determine-api-status/
- https://blog.nimblepros.com/blogs/getting-started-with-ardalis-result/
- https://github.com/ardalis/Result
- https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern
