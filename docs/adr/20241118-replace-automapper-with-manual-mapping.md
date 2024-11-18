# Replace AutoMapper with manual mapping

- Status: accepted
- Deciders: Daniel Mackay, Matt Goldman
- Date: 2024-11-18
- Tags: mappers

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/86

## Context and Problem Statement

We currently use AutoMapper to map the output of queries in the CA Template. While saving some work, this can also lead more complicate mappers, and runtime issues due to missing fields or mappings.

While mappers solve a problem in a certain set of cases, they can also introduce complexity and runtime issues, and are not a sensible default.

## Decision Drivers

- Reduce runtime errors
- Reduce tooling removing 'unused' properties

## Considered Options

1. AutoMapper
2. Manual Mapper

## Decision Outcome

Chosen option: "Option 2 - Manual Mapper", because it reduces the runtime errors, and makes both simple and complex mapping scenarios easier to understand.

### Consequences <!-- optional -->

- ✅ Once Automapper is removed, we can remove the mapping profiles from the code
- ✅ DTOs can now use records, making the code much simpler
- ✅ With much more concise code, we can fit everything in one file
- ✅ With everything in one file, we can remove a layer of folders

## Pros and Cons of the Options

### Option 1 - AutoMapper

- ✅ Less code to write for simple mapping scenarios
- ❌ Mapping becomes complex for complicated scenarios
- ❌ Can lead to runtime issues due to missing fields or mappings
- ❌ Need to learn a new library

### Option 2 - Manual Mapper

- ✅ Mapping becomes simple for both simple and complicated scenarios
- ✅ Reduced runtime issues due to missing fields or mappings
- ✅ No need to learn a new library
- ❌ More code needed for mapping

## Links

- https://www.youtube.com/watch?v=RsnEZdc3MrE
