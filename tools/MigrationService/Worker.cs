using MigrationService.Initializers;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime,
    ILogger<Worker> logger) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            var sw = Stopwatch.StartNew();
            using var scope = serviceProvider.CreateScope();
            var environment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

            var warehouseInitializer = scope.ServiceProvider.GetRequiredService<WarehouseDbContextInitializer>();
            await warehouseInitializer.EnsureDatabaseAsync(cancellationToken);
            await warehouseInitializer.RunMigrationAsync(cancellationToken);

            var catalogInitializer = scope.ServiceProvider.GetRequiredService<CatalogDbContextInitializer>();
            await catalogInitializer.EnsureDatabaseAsync(cancellationToken);
            await catalogInitializer.RunMigrationAsync(cancellationToken);

            var customersInitializer = scope.ServiceProvider.GetRequiredService<CustomersDbContextInitializer>();
            await customersInitializer.EnsureDatabaseAsync(cancellationToken);
            await customersInitializer.RunMigrationAsync(cancellationToken);

            var ordersInitializer = scope.ServiceProvider.GetRequiredService<OrdersDbContextInitializer>();
            await ordersInitializer.EnsureDatabaseAsync(cancellationToken);
            await ordersInitializer.RunMigrationAsync(cancellationToken);

            if (environment.IsDevelopment())
            {
                var products = await warehouseInitializer.SeedDataAsync(cancellationToken);
                await catalogInitializer.SeedDataAsync(products, cancellationToken);
                await customersInitializer.SeedDataAsync(cancellationToken);
                await ordersInitializer.SeedDataAsync(cancellationToken);
            }

            sw.Stop();
            logger.LogInformation($"DB creation and seeding took {sw.Elapsed} ");
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}