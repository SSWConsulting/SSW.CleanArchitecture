# Move Specifications to the Domain Layer

- Status: accepted
- Deciders: Daniel Mackay, William Libenberg, Matt Goldman, Luke Parker
- Date: 2023-08-11
- Tags: domain, specifications

Technical Story: [SSW.CleanArchitecture | Issues | 139](https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/139)

## Context and Problem Statement

The primary use cases for specifications are to:

1. Encapsulate reusable business releate queries
2. Assist with loading aggregate roots in a DRY way

## Decision Drivers

- Move as much business logic as possible to the Domain Layer
- Domain layer needs to stay persistent ignorant

## Considered Options

1. Store Specfications in the Domain Layer
2. Store Specifications in the Application Layer

## Decision Outcome

Chosen option: **Option 1 - Store Specfications in the Domain Layer**, as it allows us to move more business logic to the Domain Layer in a persistent ignorant way, and makes it easier to keep the specs up to date when the aggregate root changes.

## Pros and Cons of the Options

### Option 1 - Store Specfications in the Domain Layer

- ✅ Move more business logic to the Domain Layer in a persistent ignorant way
- ✅ Specs used to load entites are stored alongside the aggregate roots they are used to load.  This makes it easier to keep the specs up to date when the aggregate root changes.
- ❌ Added `Ardalis.Specfications` depdendency to the Domain Layer

### Option 2 - Store Specifications in the Application Layer

- ✅ Keep Domain Layer cleaner
- ❌ Specs used to load aggregate roots are now stored in a separate location from the aggregates they are used to load.  This makes it harder to keep the specs up to date when the aggregate root changes 
