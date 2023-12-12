using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SSW.CleanArchitecture.Domain.TodoItems;
using Bogus;

namespace SSW.CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext dbContext)
{
    private const int NumTodoItems = 20;

    public async Task InitializeAsync()
    {
        try
        {
            if (dbContext.Database.IsSqlServer())
            {
                await dbContext.Database.MigrateAsync();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            if (dbContext.TodoItems.Any())
                return;

            var faker = new Faker<TodoItem>()
                .CustomInstantiator(f => TodoItem.Create(
                    f.Lorem.Sentence(3), f.Lorem.Sentence(10), f.Random.Enum<PriorityLevel>(), f.Date.Future(1, DateTime.UtcNow)));

            var todoItems = faker.Generate(NumTodoItems);
            await dbContext.TodoItems.AddRangeAsync(todoItems);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while seeding the database");
            throw;
        }
    }
}