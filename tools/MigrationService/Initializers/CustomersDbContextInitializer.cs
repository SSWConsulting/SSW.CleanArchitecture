using Modules.Customers.Common.Persistence;

namespace MigrationService.Initializers;

internal class CustomersDbContextInitializer : DbContextInitializerBase<CustomersDbContext>
{
    public CustomersDbContextInitializer(CustomersDbContext dbContext) : base(dbContext)
    {
    }

    public Task SeedDataAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}