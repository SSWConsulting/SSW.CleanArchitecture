using MigrationService;
using MigrationService.Initializers;
using SSW.CleanArchitecture.Infrastructure.Persistence;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<Worker>();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

builder.Services.AddScoped<ApplicationDbContextInitializer>();
builder.AddSqlServerDbContext<ApplicationDbContext>("clean-architecture");

var host = builder.Build();

host.Run();