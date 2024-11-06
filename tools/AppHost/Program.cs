using AppHost.Commands;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// Ensure the port doesn't conflict with other docker containers (or remove it altogether)
var sqlServer = builder
    .AddSqlServer("sql", port: 1800)
    .WithLifetime(ContainerLifetime.Persistent);

var db = sqlServer
    .AddDatabase("clean-architecture")
    .WithDropDatabaseCommand();

var migrationService = builder.AddProject<MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(sqlServer);

builder
    .AddProject<WebApi>("api")
    .WithEndpoint("https", endpoint => endpoint.IsProxied = false)
    .WithReference(db)
    .WaitForCompletion(migrationService);

builder.Build().Run();