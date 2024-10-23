using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// TODO: Double check that persistent DB code works
var sqlServer = builder
    .AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sqlServer
    .AddDatabase("clean-architecture");

var migrationService = builder.AddProject<MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(sqlServer);

builder
    .AddProject<WebApi>("api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    .WaitForCompletion(migrationService);

builder.Build().Run();