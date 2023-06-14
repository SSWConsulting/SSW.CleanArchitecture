# Use EF Core and Specifications in Commands and Queries

- Status: accepted
- Deciders: Daniel Mackay, William Libenberg
- Tags: ef-core, application, commands, queries

Technical Story: [SSW.CleanArchitecture | Issues | 115](https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/115)

## Context and Problem Statement

The current approach of using Repositories and Specifications for queries did not allow for efficient data retrieval from the database.  It also provided an abstraction layer that was not providing a huge amount of value.

## Decision Drivers

- Ensure that abstractions offer significant benefit
- Ensure DB queries are efficient
- Enable smooth transition to DDD when needed

## Considered Options

1. Ardalis Repositories and Specifications
2. EF Core + Specifications
3. EF Core

## Decision Outcome

Chosen **Option 2 - EF Core + Specifications**, because it provides a good balance of query reusability, efficient querying of the DB, and allows for aggregate roots to be loaded in a consistent way.

### Consequences

- ❌ Unable to unit test commands and queries.  Will need to leverage integration tests due to EF Core.
- ❌ Application will depend on `Microsoft.EntityFrameworkCore`
- ❌ Application will depend on `Ardalis.Specification.EntityFrameworkCore`
- ⚖️ Specifications will live external to the command/query

## Pros and Cons of the Options

### 1. Ardalis Repositories and Specifications

- ✅ Easy to load Aggregate Roots
- ✅ Allows Unit testing of Commands and Queries
- ✅ Specifications can be reused for queries
- ✅ Specifications can be individually unit tested
- ❌ Cannot efficiently project onto DTO.  Ardalis.Repository executes the query internally and returns back full entity.

### 2. EF Core + Specifications

- ✅ Easy to load Aggregate Roots
- ✅ Specifications can be reused for queries
- ✅ Specifications can be individually unit tested
- ✅ Can efficiently project onto DTO
- ❌ Commands/Queries cannot be unit tested
- ❌ Dependency on EF Core added to Application

### 3. EF Core

- ✅ Easier to add new DB queries (i.e. less abstractions)
- ❌ Harder to load aggregates as you must manually include the required entities
- ❌ Cannot be unit tested
- ❌ Commands/Queries Cannot be unit tested
- ❌ Dependency on EF Core added to Application
