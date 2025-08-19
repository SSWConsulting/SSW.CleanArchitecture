![SSW Banner](https://raw.githubusercontent.com/SSWConsulting/SSW.Rules.Content/main/_docs/images/ssw-banner.png)

# SSW Clean Architecture Template

<div align="center">

[![SSW TV | YouTube](https://img.shields.io/youtube/channel/views/UCBFgwtV9lIIhvoNh0xoQ7Pg?label=SSW%20TV%20%7C%20Views&style=social)](https://youtube.com/@SSWTV)

[![Build and Test](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/build-and-test.yml)
[![Code Scanning](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/codeql.yml/badge.svg)](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/codeql.yml)
[![Package](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/package.yml/badge.svg)](https://github.com/SSWConsulting/SSW.CleanArchitecture/actions/workflows/package.yml)
[![contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen.svg?style=flat)](https://github.com/dwyl/esta/issues)
[![ADRs](https://sswconsulting.github.io/SSW.CleanArchitecture/badge.svg)](https://sswconsulting.github.io/SSW.CleanArchitecture/)

</div>

<div align="center">

![SSW.CleanArchitecture Repo Analytics](https://repobeats.axiom.co/api/embed/3abf953e88642f725e44f5b300f6eddaf8fd9bee.svg "SSW.CleanArchitecture Repo analytics")

</div>

<!-- TOC -->
- [SSW Clean Architecture Template](#ssw-clean-architecture-template)
    - [🤔 What is it?](#-what-is-it)
    - [✨ Features](#-features)
    - [🚀 Publishing Template](#-publishing-template)
        - [Process](#process)
    - [🎉 Getting Started](#-getting-started)
<!-- TOC -->

## 🤔 What is it?

This is a template for creating a new project using [Clean Architecture](https://ssw.com.au/rules/rules-to-better-clean-architecture/), leveraging [SSW Rules](https://ssw.com.au/rules) & SSW's over 30 years of experience developing software in the Microsoft space.

## ✨ Features
- 🔨 `dotnet new` cli template - to get you started quickly
- 🚀 Aspire
  - Dashboard
  - Resource orchestration
  - Observability
  - Simple dev setup - automatic provisioning of database server, schema, and data
- 🎯 Domain Driven Design Patterns
  - [Super Hero Domain](./docs/domain.md)
  - AggregateRoot
  - Entity
  - ValueObject
  - DomainEvent
- 🌐 Minimal Endpoints - because it's fast & simple. ⚡
    - Extension methods to ensure consistent HTTP Verbs & Status Codes
- 📝 OpenAPI/Scalar - easily document your API
    - as per [ssw.com.au/rules/do-you-document-your-webapi/](https://ssw.com.au/rules/do-you-document-your-webapi/)
- 🔑 Global Exception Handling - it's important to handle exceptions in a consistent way & protect sensitive information
    - Transforms exceptions into a consistent format following the [RFC7231 memo](https://datatracker.ietf.org/doc/html/rfc7231#section-6.1)
- 🗄️ Entity Framework Core - for data access
    - Comes with Migrations & Data Seeding
    - as per [ssw.com.au/rules/rules-to-better-entity-framework/](https://ssw.com.au/rules/rules-to-better-entity-framework/)
- 🧩 Specification Pattern - abstract EF Core away from your business logic
- 🔀 CQRS - for separation of concerns
    - as per [ssw.com.au/rules/keep-business-logic-out-of-the-presentation-layer/](https://ssw.com.au/rules/keep-business-logic-out-of-the-presentation-layer/)
- 📦 MediatR - for decoupling your application
- 📦 ErrorOr - fluent result pattern (instead of exceptions)
- 📦 FluentValidation - for validating requests
    - as per [ssw.com.au/rules/use-fluent-validation/](https://ssw.com.au/rules/use-fluent-validation/)
- 🆔 Strongly Typed IDs - to combat primitive obsession
    - e.g. pass `CustomerId` type into methods instead of `int`, or `Guid`
    - Entity Framework can automatically convert the int, Guid, nvarchar(..) to strongly typed ID.
- 📁 Directory.Build.Props
    - Consistent build configuration across all projects in the solution
        - e.g. Treating Warnings as Errors for Release builds
    - Custom per project
        - e.g. for all test projects we can ensure that the exact same versions of common packages are referenced
        - e.g. XUnit and NSubstitute packages for all test projects
- ⚖️ EditorConfig - comes with the [SSW.EditorConfig](https://github.com/SSWConsulting/SSW.EditorConfig)
    - Maintain consistent coding styles for individual developers or teams of developers working on the same project using different IDEs
    - as per [ssw.com.au/rules/consistent-code-style/](https://ssw.com.au/rules/consistent-code-style/)

- 🧪 Testing
    - as per [ssw.com.au/rules/rules-to-better-testing/](https://www.ssw.com.au/rules/rules-to-better-testing/)
    - Simpler Unit Tests for Application
        - **No Entity Framework mocking required** thanks to **Specifications**
        - as per [ssw.com.au/rules/rules-to-better-unit-tests/](https://www.ssw.com.au/rules/rules-to-better-unit-tests/)
    - Better Integration Tests
        - Using [Respawn](https://github.com/jbogard/Respawn) and [TestContainers](https://dotnet.testcontainers.org/)
        - Integration Tests at Unit Test speed
        - Test Commands and Queries against a Real database
        - No Entity Framework mocking required
        - No need for In-memory database provider
<!-- Commenting out pending #100     - Using [Wire-Mock](https://wiremock.org/) to mock out external services for controlled Integration Tests
      - e.g. grab real request and responses from external system and then replaying them in the tests -->
- Architecture Tests
    - Using [NetArchTest](https://github.com/BenMorris/NetArchTest)
    - Know that the team is following the same Clean Architecture fundamentals
    - The tests are automated so discovering the defects is fast
<!-- Commenting out pending #101  - Mutation Testing
    - Test our tests!
    - Helps discover the false-positives in our tests
      - you will know when your tests pass when they should have failed
    - Inserts bugs into the production code to make sure our tests are effective and testing the right behavior
    - Using [Stryker Mutator](https://stryker-mutator.io/) -->

## 🎉 Getting Started

### Prerequisites
- [Docker](https://www.docker.com/get-started/) / [Podman](https://podman.io/get-started)
- [Dotnet 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [.NET Aspire CLI](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/setup-tooling) (optional)

### Installing the Template

1. Install the SSW CA template

    ```bash
    dotnet new install SSW.CleanArchitecture.Template
    ```
    
> [!NOTE]
> The template only needs to be installed once. Running this command again will update your version of the template.

2. Create a new directory

    ```bash
    mkdir Northwind365
    cd Northwind365
    ```

3. Create a new solution

    ```bash
    dotnet new ssw-ca
    ```

> [!NOTE]
> `name` is optional; if you don't specify it, the directory name will be used as the solution name and project namespaces.


Alternatively, you can specify the `name` and `output` directory as follows:
    
```bash
dotnet new ssw-ca --name {{SolutionName}} --output .\
```

### Adding a Feature

1. Create a query

    ```bash
    cd src/Application/UseCases
    mkdir {{FeatureName}}
    cd {{FeatureName}}
    dotnet new ssw-ca-query --name {{QueryName}} --entityName {{Entity}} --slnName {{SolutionName}}
    ```

2. Create a command

    ```bash
    cd src/Application/UseCases
    mkdir {{FeatureName}}
    cd {{FeatureName}}
    dotnet new ssw-ca-command --name {{CommandName}} --entityName {{Entity}} --slnName {{SolutionName}}
    ```

### Running the Solution

1. Change directory

    Windows:
    ```ps
    cd tools\AppHost\
    ```

    Mac/Linux:
    ```bash
    cd tools/AppHost/
    ```

2. Run the solution

    ```bash
    dotnet run
    ```

> [!NOTE]
> The first time you run the solution, it may take a while to download the docker images, create the DB, and seed the data.

3. Open https://localhost:7255/scalar/v1 in your browser to see it running ️🏃‍♂️

### EF Migrations
Due to .NET Aspire orchestrating the application startup and migration runner, EF migrations need to be handled a little differently to normal.

#### Adding a Migration
Adding new migrations is still the same old command you would expect, but with a couple of specific parameters to account for the separation of concerns. This can be performed via native dotnet tooling or through the Aspire CLI:

1. Run either of following commands from the root of the solution.

```bash
dotnet ef migrations add YourMigrationName --project ./src/Infrastructure/Infrastructure.csproj --startup-project ./src/WebApi/WebApi.csproj --output-dir ./Persistence/Migrations
```

```bash
aspire exec --resource api -- dotnet ef migrations add YourMigrationName --project ../Infrastructure/Infrastructure.csproj --output-dir ./Persistence/Migrations
```

#### Applying a Migration
.NET Aspire handles this for you - just start the project!

#### Removing a Migration
This is where things need to be done a little differently and requires the Aspire CLI.

1. Enable the `exec` function:

```bash
aspire config set features.execCommandEnabled true
```

2. Pass the EF migration shell command through Aspire from the root of the solution:

```bash
aspire exec --resource api -- dotnet ef migrations remove --project ..\Infrastructure --force
```
> [!NOTE]
> The `--force` flag is needed because .NET Aspire will start the application when this command is run, which triggers the migrations to run. This will apply your migrations to the database, and make EF Core unhappy when it tries to delete the latest migration. This should therefore be used with caution - a safer approach is to "roll forward" and create new migrations that safely undo the undesired change(s).

## Deploying to Azure

The template can be deployed to Azure via
the [Azure Developer CLI (AZD)](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows,brew-mac,script-linux&pivots=os-mac).
This will setup the following:

- Azure App Services: API + MigrationService
- Azure SQL Server + Database: Data storage
- Application Insights + Log Analytics: For monitoring and logging
- Managed Identities: For secure access to Azure resources
- Azure Container Registry: For storing Docker images

### Steps to Deploy

1. Authenticate with Azure

    ```bash
    azd auth login
    ```

2. Initialize AZD for the project

    ```bash
    azd init
    ```

3. Update environment variables

    ```bash
    azd env set ASPNETCORE_ENVIRONMENT Development
    ```

4. Deploy to Azure

    ```bash
    azd up
    ```

> [!NOTE]
> `azd up` combines `azd provision` and `azd deploy` commands to create the resources and deploy the application. If running this from a CI/CD
> pipeline, you can use `azd provision` and `azd deploy` separately in the appropriate places.

## 🚀 Publishing Template

Template will be published to NuGet.org when changes are made to `CleanArchitecture.nuspec` on the `main` branch.

### Process

1. Update the `version` attribute in `CleanArchitecture.nuspec`
2. Merge your PR
3. `package` GitHub Action will run and publish the new version to NuGet.org
4. Create a GitHub release to document the changes

> [!NOTE]
> We are now using CalVer for versioning. The version number should be in the format `YYYY.M.D` (e.g. `2024.2.12`).

<!-- TODO Issue #99: Getting Started using the dotnet new template -->

## 🎓 Learn More

### Training

If you're interested in learning more about Clean Architecture SSW offers two events:

- [SSW 1-day Clean Architecture Superpowers Tour](https://www.ssw.com.au/events/clean-architecture-superpowers-tour)
- [SSW 2-day Clean Architecture Workshop](https://www.ssw.com.au/events/clean-architecture-workshop)

### Learning Resources

You're interested learning more about Clean Architecture, please see this excellent video by Matt Goldman:

* [Clean Architecture with ASP.NET Core and MAUI](https://www.youtube.com/live/K9ryHflmQJE?si=VC2FtSZiAA3CxSsK)

Alternatively, SSW has many great rules about Clean Architecture:

* [SSW Rules - Clean Architecture](https://www.ssw.com.au/rules/rules-to-better-clean-architecture/)

You can also find a collection of commumity projects built on Clean Architecture here:

* [Awesome Clean Architecture](https://github.com/SSWConsulting/awesome-clean-architecture)

## 🤝 Contributing

Contributions, issues and feature requests are welcome! See [Contributing](./CONTRIBUTING.md) for more information.
