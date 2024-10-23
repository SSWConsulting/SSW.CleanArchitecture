using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// TODO: Double check that persistent DB code works
var db = builder
    .AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("clean-architecture");

var migrationService = builder.AddProject<MigrationService>("migrations")
    .WithReference(db) // TODO: Should this reference the DB or Server?
    .WaitFor(db);

builder
    .AddProject<WebApi>("api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WaitForCompletion(migrationService);

builder.Build().Run();