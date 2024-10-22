using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var db = builder
    .AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("clean-architecture");

// TODO: https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/226
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