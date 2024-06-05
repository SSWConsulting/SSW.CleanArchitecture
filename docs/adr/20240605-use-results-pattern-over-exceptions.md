# Use Results Pattern Over Exceptions

- Status: proposed
- Deciders: {{ DECIDERS }} <!-- optional: list everyone involved in the decision -->
- Date: 2024-06-05
- Tags: result-pattern, exceptions <!-- optional: space and/or comma-separated list of tags -->

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/242 <!-- optional: description | ticket/issue URL -->

## Context and Problem Statement

Using exceptions allows us to have global handling for certain types of errors (server error, bad request, not found, etc). They keep our code minimal as we don't need to add specific handling into every API endpoint. However, we still need to ensure these status codes are documented correctly.

The problem with this approach is that we're using exceptions as flow control. In other words, if a behavior is expected in your application (like a validation error), this is not an exceptional circumstance.

According to David Fowler exceptions are **very expensive** in ASP.NET Core.

There are several problems with using exceptions as flow control:

- Performance - Exceptions are also very expensive to throw and catch
- Breaks Principle of Least Astonishment - It's not obvious what exceptions can be thrown from a method. You need to look deep into the code.
- Flow Control - Exceptions are complicated goto statements
- Exception Groups - Exceptions can be grouped into 'expected' and 'unexpected' exceptions. It can be difficult to know which is which.

## Considered Options

1. Use Exceptions for Validation Flow Control
2. Use Result Pattern for Validation and Flow Control

## Decision Outcome

Chosen option: "Option 2 - Use Result Pattern for Validation and Flow Control", because exceptions should only be used for exceptional circumstances, and NOT for flow control.

### Consequences <!-- optional -->

- Dependency on additional nuget packages

## Pros and Cons of the Options <!-- optional -->

### Option 1 - Use Exceptions for Validation Flow Control

As per the current implementation.

- ✅ Simple
- ✅ Less code
- ❌ Performance cost
- ❌ Using exceptions as flow control (expensive 'goto' statements)
- ❌ Breaks Principle of Least Astonishment
- ❌ Exception Groups - not obvious which exceptions are expected and which are unexpected

### Option 2 - Use Result Pattern for Validation and Flow Control

- ✅ Improved Performance - no exceptions thrown
- ✅ Explicit flow control
- ✅ Adheres to the Principle of Least Astonishment
- ✅ Exception Groups - it's clear that exceptions are real problems
- ❌ More verbose code in the minimal API
- ❌ It's possible to forget to check specific results

The result pattern could be implemented via:

- [Ardalis.Result](https://github.com/ardalis/Result)
- [Fluent Results](https://github.com/altmann/FluentResults)
- [OneOf](https://github.com/mcintyre321/OneOf)

## Links <!-- optional -->

- https://ardalis.com/avoid-using-exceptions-determine-api-status/
- https://blog.nimblepros.com/blogs/getting-started-with-ardalis-result/
- https://github.com/ardalis/Result
- https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern
- https://www.dandoescode.com/blog/minimal-apis-typed-results-and-open-api
