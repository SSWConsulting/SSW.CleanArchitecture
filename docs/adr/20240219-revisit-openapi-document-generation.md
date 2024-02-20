# Revisit OpenAPI document generation

- Status: accepted
- Deciders: Daniel M, William L, Matt W
- Date: 2024-02-19
- Tags: api, openapi

Technical Story: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/202

## Context and Problem Statement

We are not generating client side code anymore (which was one of the reasons we were using NSwag). Now that we are only generating the OpenAPI Spec, is it better to use NSwag or Swashbuckle to generate the spec.
We still want an API UI
Check with @matt-goldman , @drwharris , Alex Rothwell on feedback/opinions around this
Enum generation needs to also be considered as this is often a painpoint

## Decision Drivers <!-- optional -->

- Extensibility
- Compatibility
- Project activity

## Considered Options

1. NSwag + NSwag.MSBuild
1. Swashbuckle
1. NSwag (runtime only)

## Decision Outcome

Chosen option: "NSwag (runtime only)", because the NSwag repo is more active than the Swashbuckle one. The removal of NSwag.MSBuild also improved build times. 

### Consequences <!-- optional -->

- ✅ Clients that generate code can use nswag to generate their own code with a nice enum experience

## Pros and Cons of the Options <!-- optional -->

### OPTION 1: NSwag + NSwag.MSBuild

Leave as is.

- ✅ Nothing to change
- ✅ `specification.json` is served as a static file = performance
- ✅ Project is actively developed
- ❌ Slower build times
- ❌ We aren't generating clients as part of the API - no benefit to the msbuild step
- ❌ `nswag.config` doesn't have great documentation (or a schema)

### OPTION 2: Swashbuckle

This is the one included with the .NET templates

- ✅ Included in the templates
- ❌ Doesn't support DateOnly/TimeOnly OOTB - need [extra package ](https://github.com/maxkoshevoi/DateOnlyTimeOnly.AspNet). What other packages will we need?
- ❌ Last commit on repo 2023-01-11 (13 months ago)
- ❌ Repo owner cannot justify spending time on the project - https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/2759#issuecomment-1936136992

### OPTION 3: NSwag (runtime only)

Same as option 1 but without MSBuild step

- ✅ Minimal changes to repo - less code
- ✅ Faster build times than option 1
- ✅ Project is actively developed
- ✅ No need for `nswag.json` - adds confusion as it doesn't have great documentation (or a schema)
- ❌ Now serving a runtime generated file instead of a static file
- ❌ NSwag still uses Newtonsoft.Json internally - although it is moving to System.Text.Json in the next major version v15


