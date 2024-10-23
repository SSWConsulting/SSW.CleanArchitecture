using Bogus;
using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace MigrationService.Initializers;

internal class WarehouseDbContextInitializer : DbContextInitializerBase<WarehouseDbContext>
{
    private const int NumProducts = 20;
    private const int NumAisles = 10;
    private const int NumShelves = 5;
    private const int NumBays = 20;

    public WarehouseDbContextInitializer(WarehouseDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Product>> SeedDataAsync(CancellationToken cancellationToken)
    {
        var strategy = DbContext.Database.CreateExecutionStrategy();
        IReadOnlyList<Product> products = [];
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            await SeedAisles();
            products = await SeedProductsAsync();
            await DbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });

        return products;
    }

    private async Task SeedAisles()
    {
        if (await DbContext.Aisles.AnyAsync())
            return;

        for (var i = 1; i <= NumAisles; i++)
        {
            var aisle = Aisle.Create($"Aisle {i}", NumBays, NumShelves);
            DbContext.Aisles.Add(aisle);
        }

        await DbContext.SaveChangesAsync();
    }

    private async Task<IReadOnlyList<Product>> SeedProductsAsync()
    {
        if (await DbContext.Products.AnyAsync())
            return [];

        // TODO: Consider how to handle integration events that get raised and handled

        var skuFaker = new Faker<Sku>()
            .CustomInstantiator(f => Sku.Create(f.Commerce.Ean8())!);

        var faker = new Faker<Product>()
            .CustomInstantiator(f => Product.Create(f.Commerce.ProductName(), skuFaker.Generate()));

        var products = faker.Generate(NumProducts);
        DbContext.Products.AddRange(products);
        await DbContext.SaveChangesAsync();

        return products;
    }
}