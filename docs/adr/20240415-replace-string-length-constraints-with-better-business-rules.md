# Replace database string length constraints with better business rules

- Status: accepted
- Deciders: Matt Wicks, Matt Goldman, Chris Clement, Daniel Mackay
- Date: 2024-04-15
- Tags: database, domains

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/285

## Context and Problem Statement

As we are moving forward to make sure that CleanArchitecture template is lean and can be used
straightforward in modern projects, we are going to drop technical constraints such as string length in names and description
and instead opt to add more rules which adhere more to the example business logic (e.g. a hero name and alias cannot be the same).

## Decision Drivers <!-- optional -->

- Make sure faster setup time for CleanArchitecture templates in modern projects
- Reduce arbitrary technical concerns in database that does not stem from thoughtful requirements

## Considered Options

1. (Recommended) Replace string length constraints with non-technical requirement
2. Drop string length constraints and do not replace them
3. Keep string length constraints but split the constants to each domain

## Decision Outcome

Chosen option: "Replace string length constraints with non-technical requirement", because we want to
keep the example of guard clauses in the code.

## Pros and Cons of the Options <!-- optional -->

### 1. Replace string length constraints with non-technical requirement

- ✅ Guard clauses example
- ✅ No shared constraint values
- ❌ No example of linking domain constraints to infrastructure implementation

### 2. Drop string length constraints and do not replace them

- ✅ No shared constants
- ❌ No guard clauses example

### 3. Keep string length constraints but split the constants to each domain

- ✅ No changes to code
- ✅ Guard clauses example
- ❌ Shared constraint values for different entities / across aggregate root
- ❌ Template starts with limit to string length which might not be needed / different value needed for different projects

