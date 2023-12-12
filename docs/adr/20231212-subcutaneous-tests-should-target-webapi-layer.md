# Subcutaneous Tests Should Target WebApi Layer

- Status: accepted
- Deciders: Daniel M, William L, Matt W
- Date: 2023-12-12
- Tags: tests

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/201

## Context and Problem Statement

Previously, the Subcutaneous tests were targeting the Application Layer. This was ok, but skipped the WebApi layer, which is the layer that the end user will be interacting with. This means that the tests were not testing the full stack, and we were not getting the full benefit of the Subcutaneous tests.

## Decision Drivers <!-- optional -->

- Subcutaneous tests should test as much of the stack as possible
- Some element of contract testing is required

## Considered Options

1. Leave the Subcutaneous tests targeting the Application Layer
2. Change Subcutaneous tests to target the WebApi Layer

## Decision Outcome

Chosen option: "Option 2: Change Subcutaneous tests to target the WebApi Layer", because the subcutaneous tests should be testing as much of the stack as possible.  There is no downside to this option.

## Pros and Cons of the Options

### Option 1: Leave the Subcutaneous tests targeting the Application Layer

- ✅ Logic and Internal Infrastructure tested
- ❌ Serialization not tested
- ❌ Contract testing not tested
- ❌ Web Authentication/Authorization not tested

### Option 2: Change Subcutaneous tests to target the WebApi Layer

- ✅ Logic and Internal Infrastructure tested
- ✅ Serialization tested
- ✅ Web Authentication/Authorization tested
- ⚠️ Some contract testing (routes only)
