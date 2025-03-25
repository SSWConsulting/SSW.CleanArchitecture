# Migrate to Awesome Assertions

- Status: accepted
- Deciders: Daniel Mackay, Luke Cooke, Matt Wicks, Anton Polkanov, Matt Parker 
- Date: 2025-03-26
- Tags: testing, unit-tests, assertions, awesome-assertions, fluent-assertions

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/pull/497

## Context and Problem Statement

We have been using FluentAssertions for our unit tests. 

This library is great however, the project has now switched to a paid model. Versions 8 and beyond are free for open-source projects and non-commercial use, but commercial use requires a paid licence.

Version 7 will remain fully open-source indefinitely and receive bugfixes and other important corrections.

## Decision Drivers

- Free for commercial use
- Concise and readable assertions

## Considered Options

1. Shouldly
2. Fluent Assertions (free version)
3. Fluent Assertions (paid version)
4. Native test framework assertions (e.g. xUnit/NUnit/TUnit)
5. Awesome Assertions

## Decision Outcome

Chosen option: "Option 5 - Awesome Assertions", because we get the same experience as FluentAssertions, but it is free for commercial use. It is also easy to migrate from FluentAssertions to Awesome Assertions.

## Pros and Cons of the Options <!-- optional -->

### Option 1 - Shouldly

https://docs.shouldly.org/

- ✅ Good assertion library
- ✅ Free for commercial use
- ❌ Would require a lot of work to migrate from FluentAssertions
- ❌ Custom extensions would need to be rewritten

### Option 2 - Fluent Assertions (free version)

https://xceed.com/products/unit-testing/fluent-assertions/

- ✅ Great assertion library
- ✅ Version 7 is Free for open-source projects and non-commercial use
- ❌ Easy to upgrade to version 8, resulting in substantial licence fees

### Option 3 - Fluent Assertions (paid version)

https://xceed.com/products/unit-testing/fluent-assertions/

- ✅ Great assertion library
- ❌ Substantial licence fees for commercial use. ($130 per developer per year. For SSW this would cost $7,410 per year for 57 developers)

### Option 4 - Native test framework assertions (e.g. xUnit/NUnit/TUnit)

- ✅ Free for commercial use
- ✅ No additional dependencies
- ❌ Assertions are not as readable as FluentAssertions
- ❌ Would require a lot of work to migrate from FluentAssertions
- ❌ Custom extensions would need to be rewritten

### Option 5 - Awesome Assertions

https://awesomeassertions.org/

- ✅ Great assertion library
- ✅ Free for commercial use
- ✅ Easy to migrate from FluentAssertions
- ✅ Custom extensions will continue to work (with minor modifications)
