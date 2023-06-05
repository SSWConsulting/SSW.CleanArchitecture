<img src="https://raw.githubusercontent.com/SSWConsulting/SSW.Rules.Content/main/_docs/images/ssw-banner.png">

# SSW Clean Architecture Template



<div align="center">

[![SSW TV | YouTube](https://img.shields.io/youtube/channel/views/UCBFgwtV9lIIhvoNh0xoQ7Pg?label=SSW%20TV%20%7C%20Views&style=social)](https://youtube.com/@SSWTV)


[![.NET](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/dotnet.yml/badge.svg)](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/dotnet.yml)
[![Code Scanning](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/codeql.yml/badge.svg)](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/codeql.yml)
[![Package](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/package.yml/badge.svg)](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/package.yml)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/dwyl/esta/issues)

</div>

<!-- TOC -->
* [SSW Clean Architecture Template](#ssw-clean-architecture-template)
  * [🤔 What is it?](#-what-is-it)
  * [✨ Features](#-features)
  * [🚀 Publishing Template](#-publishing-template)
    * [Process](#process)
<!-- TOC -->

## 🤔 What is it?

This is a template for creating a new project using [Clean Architecture](https://ssw.com.au/rules/rules-to-better-clean-architecture/), leveraging [SSW Rules](https://ssw.com.au/rules) & SSW's over 30 years of experience developing software in the Microsoft space. 

## ✨ Features

- ⚖️ EditorConfig - comes with the [SSW.EditorConfig](https://github.com/SSWConsulting/SSW.EditorConfig)
  - Maintain consistent coding styles for individual developers or teams of developers working on the same project using different IDEs
- 📦 Slim - no authentication provider, no authorization & no UI framework
  - You can add these yourself or use one of our reference architectures from [awesome-clean-architecture](https://github.com/SSWConsulting/awesome-clean-architecture)
- 🌐 Minimal Endpoints - because it's fast & simple. ⚡
  - Extension methods to ensure consistent HTTP Verbs & Status Codes
- 🔑 Global Exception Handling - it's important to handle exceptions in a consistent way & protect sensitive information
  - Transforms exceptions into a consistent format following the [RFC7231 memo](https://datatracker.ietf.org/doc/html/rfc7231#section-6.1) 
- 📝 OpenAPI/Swagger - easily document your API
- 🗄️ Entity Framework Core - for data access
  - Comes with Migrations & Data Seeding
- 🧩 Specification Pattern - abstract EF Core away from your business logic
- 🔀 CQRS - for separation of concerns
- 📦 MediatR - for decoupling your application
- 📦 FluentValidation - for validating requests
- 📦 AutoMapper - for mapping between objects
- 🆔 Strongly Typed IDs - to combat primitive obsession
  - e.g. pass `CustomerId` type into methods instead of `int`, or `Guid`
  - Entity Framework can automatically convert the int, Guid, nvarchar(..) to strongly typed ID.
- 🔨 `dotnet new` cli template - to get you started quickly
- 📁 Directory.Build.Props
  - Consistent build configuration across all projects in the solution
    - e.g. Treating Warnings as Errors for Release builds
  - Custom per project
    - e.g. for all test projects we can ensure that the exact same versions of common packages are referenced
    - e.g. XUnit and NSubstitute packages for all test projects
- 🧪 Testing
  - Simpler Unit Tests for Application
    - **No Entity Framework mocking required** thanks to **Specifications**
  - Better Integration Tests
    - Using [Respawn](https://github.com/jbogard/Respawn) and [TestContainers](https://dotnet.testcontainers.org/)
    - Integration Tests at Unit Test speed
    - Test Commands and Queries against a Real database
    - No Entity Framework mocking required
    - No need for In-memory database provider
    - Using [Wire-Mock](https://wiremock.org/) to mock out external services for controlled Integration Tests
      - e.g. grab real request and responses from external system and then replaying them in the tests
  - Architecture Tests
    - Using [NetArchTest](https://github.com/BenMorris/NetArchTest)
    - Know that the team is following the same Clean Architecture fundamentals
    - The tests are automated so discovering the defects is fast
  - Mutation Testing
    - Test our tests!
    - Helps discover the false-positives in our tests
      - you will know when your tests pass when they should have failed
    - Inserts bugs into the production code to make sure our tests are effective and testing the right behavior
    - Using [Stryker Mutator](https://stryker-mutator.io/)

## 🚀 Publishing Template

Template will be published to NuGet.org when changes are made to `CleanArchitecture.nuspec` on the `main` branch.

### Process

1. Update the `version` attribute in `CleanArchitecture.nuspec`
2. Merge your PR
3. `package` GitHub Action will run and publish the new version to NuGet.org

<!-- TODO Issue #99: Getting Started using the dotnet new template -->