# Migrate from xUnit to TUnit

- Status: accepted
- Deciders: Daniel Mackay, Anton Polkanov, Luke Cooke
- Date: 2025-03-21
- Tags: xunit, nunit, tunit, tests

## Context and Problem Statement

xUnit has been a populate testing framework for a some time. There are some good parts to the framework, but also some rough edges in terms of unintuitive code to share state at different levels (i.e. between tests and between classes). 

## Decision Drivers

- Fast test execution (ideally)
- Ease of use - sharing state & integration test setup/cleanup
- Code coverage support in IDEs

## Considered Options

1. xUnit v3
2. NUnit
3. TUnit

## Decision Outcome

Chosen option: "Option 3 - TUnit", because it is blazingly fast, and the hooks it provides makes setting up integration tests simpler and easier to understand.

### Consequences

- ✅ {{ POSITIVE CONSEQUENCE 1 }}
- ❌ {{ NEGATIVE CONSEQUENCE 1 }}

## Pros and Cons of the Options <!-- optional -->

### Option 1 - xUnit v3

- ✅ 4.3k stars (over 14 years)
- ✅ By default, state is not shared between tests
- ✅ Well supported test framework
- ✅ Code coverage works in IDEs
- ❌ Complicated sharing of state
- ❌ Maintained mainly by 1 person (Brad Wilson)

[//]: # (TODO: show screenshot of integration test setup)

### Option 2 - NUnit

- ✅ 2.6k stars (over 14 years)
- ✅ Rich set of test features
- ✅ Easy to customise setup / clean up at different levels (i.e. per test, class, or assembly)
- ❌ By default, state is shared between tests

### Option 3 - TUnit

- ✅ 3k stars (over 1 year)
- ✅ By default, state is not shared between tests
- ✅ Blazingly fast, and support for AoT compilation
- ✅ Rich set of test features
- ✅ Easy to customise setup / clean up at different levels (i.e. per test, class, or assembly. This is great for integration tests)
- ✅ Leverages new .NET testing framework
- ✅ Excellent Documentation
- ❌ Code coverage doesn't work in IDEs
- ❌ Maintained mainly by 1 person (Tom Hurst)

[//]: # (TODO: show screenshot of integration test setup)

### Comparison

[//]: # (TODO: Add comparison of all 3)

## Links <!-- optional -->

- [xUnit](TODO)
- [NUnit](TODO)
- [TUnit](https://thomhurst.github.io/TUnit/)
