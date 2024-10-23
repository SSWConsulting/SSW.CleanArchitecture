# Use .NET OpenAPI and Scalar over NSwag and Swagger

- Status: accepted
- Deciders: Daniel Mackay, Matt Goldman, Matt Parker
- Date: 2024-10-23
- Tags: openapi, swagger, nswag

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/383

## Context and Problem Statement

Currently, we use NSwag to generate our OpenAPI (Swagger) specification file. .NET 9 has introduced a new way to generate OpenAPI spec files using the `Microsoft.AspNetCore.OpenApi` library. However, this does not include the Swagger UI that previously came with NSwag. We need to decide if we should continue to use NSwag or switch to the new `Microsoft.AspNetCore.OpenApi` library, and if we should use the Swagger UI or another tool to view the OpenAPI spec.

## Considered Options

1. NSwag with Swagger UI
2. `Microsoft.AspNetCore.OpenApi` with Swagger UI
3. `Microsoft.AspNetCore.OpenApi` with [Scalar](https://github.com/scalar/scalar)

## Decision Outcome

Chosen option: "Option 3 - `Microsoft.AspNetCore.OpenApi` with [Scalar](https://github.com/scalar/scalar)", because we should use the built-in .NET library for generating OpenAPI spec files, and Scalar is a modern, lightweight, and fast alternative to Swagger UI.

## Pros and Cons of the Options <!-- optional -->

### 1. NSwag with Swagger UI

- ✅ Tried and tested solution
- ❌ Does not use the 1st Party .NET library
- ❌ Dependency on additional NuGet package(Nswag.AspNetCore, Nswag.MSBuild)

### 2. `Microsoft.AspNetCore.OpenApi` with Swagger UI

- ✅ Uses the 1st Party .NET library
- ✅ Uses well-known Swagger UI
- ❌ Dependency additional NuGet package (Swashbuckle)

### 3. `Microsoft.AspNetCore.OpenApi` with [Scalar](https://github.com/scalar/scalar)

- ✅ Uses the 1st Party .NET library
- ✅ Modern, lightweight, and fast alternative to Swagger UI
- ❌ Dependency additional NuGet package (Scalar.AspNetCore)
- ❌ Scalar - Relatively new and unknown compared to Swagger UI

## Links <!-- optional -->

- https://github.com/scalar/scalar
- https://github.com/RicoSuter/NSwag/
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/openapi/aspnetcore-openapi?view=aspnetcore-9.0
