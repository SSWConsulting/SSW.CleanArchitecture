using Bogus;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Categories.Domain;
using Modules.Catalog.Common.Persistence;
using Modules.Warehouse.Products.Domain;

namespace MigrationService.Initializers;

internal class CatalogDbContextInitializer : DbContextInitializerBase<CatalogDbContext>
{
    private const int NumCategories = 10;

    public CatalogDbContextInitializer(CatalogDbContext dbContext) : base(dbContext)
    {
    }

    public async Task SeedDataAsync(IReadOnlyList<Product> products, CancellationToken cancellationToken)
    {
        var strategy = DbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Seed the database
            await using var transaction = await DbContext.Database.BeginTransactionAsync(cancellationToken);
            var categories = await SeedCategories();
            await SeedProductsAsync(products, categories);
            await DbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    private async Task<IReadOnlyList<Category>> SeedCategories()
    {
        if (await DbContext.Categories.AnyAsync())
            return [];

        var categoryFaker = new Faker<Category>()
            .CustomInstantiator(f => Category.Create(f.Commerce.Categories(1).First()!));

        var categories = categoryFaker.Generate(NumCategories);
        DbContext.Categories.AddRange(categories);
        await DbContext.SaveChangesAsync();

        return categories;
    }

    private async Task SeedProductsAsync(IEnumerable<Product> warehouseProducts, IEnumerable<Category> categories)
    {
        if (await DbContext.Products.AnyAsync())
            return;

        var categoryFaker = new Faker<Category>()
            .CustomInstantiator(f => f.PickRandom(categories));

        // Usually integration events would propagate products to the catalog
        // However, to simplify test data seed, we'll manually pass products into the catalog
        foreach (var warehouseProduct in warehouseProducts)
        {
            var catalogProduct = Modules.Catalog.Products.Domain.Product.Create(
                warehouseProduct.Name,
                warehouseProduct.Sku.Value,
                warehouseProduct.Id);

            var productCategory = categoryFaker.Generate();
            catalogProduct.AddCategory(productCategory);

            DbContext.Products.Add(catalogProduct);
        }

        await DbContext.SaveChangesAsync();
    }
}