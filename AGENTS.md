# SSW Clean Architecture - AGENTS.md

This is a .NET 10 Clean Architecture template using Domain-Driven Design (DDD) tactical patterns, CQRS with MediatR, and .NET Aspire for orchestration.

## Architecture Overview

```
src/
├── Domain/          # Entities, Value Objects, Aggregates, Domain Events, Specifications
├── Application/     # Use Cases (Commands/Queries), Interfaces, Validators
├── Infrastructure/  # EF Core, External Services, Persistence
├── WebApi/          # Minimal API Endpoints, Extensions
tools/
├── AppHost/         # .NET Aspire orchestration
├── MigrationService/ # EF Core migrations runner
```

**Dependency Flow**: WebApi → Application → Domain ← Infrastructure

## Running the Solution

```bash
aspire run
```

Aspire handles database provisioning, migrations, and seeding automatically. API available at `https://localhost:7255/scalar/v1`.

## Further Reading

- Implementation patterns (commands, queries, domain entities, endpoints, conventions, migrations) → `src/AGENTS.md`
- Testing patterns (integration, unit, architecture tests) → `tests/AGENTS.md`

## ADRs

Architectural decisions documented in `docs/adr/`. Key decisions:
- Results pattern over exceptions
- Vogen for strongly-typed IDs
- Manual mapping over AutoMapper
- Specifications in Domain layer
