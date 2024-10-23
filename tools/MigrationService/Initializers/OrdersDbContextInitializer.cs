using Modules.Orders.Common.Persistence;

namespace MigrationService.Initializers;

internal class OrdersDbContextInitializer : DbContextInitializerBase<OrdersDbContext>
{
    public OrdersDbContextInitializer(OrdersDbContext dbContext) : base(dbContext)
    {
    }

    public Task SeedDataAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}