# Use Domain-Driven Design Tactical Patterns

- Status: Pending <!-- optional: draft | proposed | rejected | accepted | deprecated | … | superseded by [xxx](yyyymmdd-xxx.md) -->
- Deciders: {{ TBC }} <!-- optional: list everyone involved in the decision -->
- Date: 2024-04-04 <!-- optional. YYYY-MM-DD when the decision was last updated. To customize the ordering without relying on Git creation dates and filenames -->
- Tags: ddd <!-- optional: space and/or comma-separated list of tags -->

Technical Story: {{ TECH STORY }} <!-- optional: description | ticket/issue URL -->

## Context and Problem Statement

Currently, the template has is mostly geared towards building applications with an anemic Domain model.  This is fine for simple applications, but not for complex applications with many business rules.

Instead, we would like to default to using DDD in the template and provide a good example of building applications in that manner.

## Considered Options

1. Anemic Domain Model
2. Rich Domain Model with DDD

## Decision Outcome

Chosen option: "Option 2 - Rich Domain Model with DDD", because it helps set developers up for success when building complex applications.  It's easier to go from a rich domain model to an anemic domain model than the other way around.

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
