using AppHost.Commands;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder
    // If desired, set SQL Server Port to a constant value
    .AddSqlServer("sql" /*, port: 1800*/)
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