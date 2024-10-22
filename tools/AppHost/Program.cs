using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder
    .AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sqlServer.AddDatabase("CleanArchitecture");

// var migrationService = builder.AddProject<MigrationService>("migrations")
//     .WithReference(warehouseDb)
//     .WithReference(catalogDb)
//     .WithReference(customersDb)
//     .WithReference(ordersDb)
//     .WaitFor(sqlServer);

builder
    .AddProject<WebApi>("api")
    .WithExternalHttpEndpoints()
    .WithReference(db)
    // .WaitForCompletion(migrationService)
    ;

builder.Build().Run();