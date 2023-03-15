# SSW.CleanArchitectureV2

## Pains with Clean Architecture v1

- No default coding standards included
- Controllers can still get filled up with countless actions - hard to group / split actions out
- OpenAPI Spec Documentation is hard work to maintain and often wrong or incomplete
  - Wrong response types
  - Incomplete Input / Output schemas
- Not utilizing Domain Events enough
  - No good reference example or real-world examples for utilizing Domain Events
- Unit tests and Integration tests are difficult to implement
  - Mocking Entity Framework's `DbSet` is risky as we make a lot of assumptions about how it works
- Single primary key data type for all entities in the database
  - e.g. `int` for everything or `guid` for everything
  - Difficult to change between types or to add secondary keys with different types
- Hard to get started on a Client Project using the template
  - need to remove a lot of demo code
  - need to remove Default Identity Server for Auth
  - need to remove the Angular application - when we only need a WebAPI or host the UI app separately

## Features

### General

- **EditorConfig**
  - Using [SSW.EditorConfig](https://github.com/SSWConsulting/SSW.EditorConfig)
  - Maintain consistent coding styles for individual developers or teams of developers working on the same project using different IDEs
- **Directory.build.props**
  - Consistent build configuration across all projects in the solution
    - e.g. Treating Warnings as Errors for Release builds
  - Custom per project
    - e.g. for all test projects we can ensure that the exact same versions of common packages are referenced
    - e.g. `XUnit` and `NSubstitute` packages for all test projects
- Dotnet CLI templates that can let us choose Auth and UI projects to generate with the base Clean Architecture template

### Web API

- NSwag / SwaggerUI
  - To produce the OpenAPI `specification.json` file that can be used to generate TypeScript or C# clients
  - Serve as API Documentation for API Integration development
- **Minimal APIs**
  - Lightweight and more performant than MVC Controllers
  - Easier to group related endpoints (or easier to separate non-related endpoints)
- **Consistent REST API status codes and response types**
  - Using best practice HTTP Status Codes for each HTTP Verb (`GET`, `POST`, `PUT`, `DELETE`)
- No default auth provider
  - Authentication provider examples will be in separate GitHub repositories on [awesome-clean-architecture](https://github.com/SSWConsulting/awesome-clean-architecture)
- No default UI framework
  - UI framework reference examples will be in separate GitHub repositories on [awesome-clean-architecture](https://github.com/SSWConsulting/awesome-clean-architecture)

### Application

- **Specifications**
  - Using [Ardalis.Specification](https://github.com/ardalis/Specification)
  - Removes the need for Entity Framework dependency in the Application Layer
  - Makes Unit Testing easier - no more assumptions about how Entity Framework works when mocking out the `DbSet` functionality
  - Specifications can (and should) have their own Integration Tests.
  - Specifications can be tested directly and not via a Command or Query that uses them
  - Reuse common queries throughout the application
- **CQRS compliance**
  - Using Read-write repositories for Commands
  - Using Read-only repositories for Queries (no accidental side-effects against thee data)
- Fluent Validation
- AutoMapper

### Domain

- **Strongly Typed IDs**
  - To combat [Primitive Obsession](https://blog.ndepend.com/code-smell-primitive-obsession-and-refactoring-recipes/) (pass `CustomerId` type into methods instead of `int`, or `Guid`)
  - Entity Framework can automatically convert the `int`, `Guid`, `nvarchar(..)` to strongly typed ID.
    - e.g. `13` -> `new CustomerId(13)`
  - Can be configured per entity!

### Infrastructure

- Entity Framework Core
  - **Transient Fault Handling**
- Migrations
- Data Seeding
- Model Configuration

### Testing

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
