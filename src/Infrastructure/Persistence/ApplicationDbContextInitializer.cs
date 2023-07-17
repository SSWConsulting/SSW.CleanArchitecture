using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FluentValidation.Validators;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _dbContext;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_dbContext.Database.IsSqlServer())
                await _dbContext.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while migrating or initializing the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            if (_dbContext.TodoItems.Any())
                return;

            _dbContext.TodoItems.Add(TodoItem.Create("Learn Clean Architecture", "Learn how to build a Clean Architecture application", PriorityLevel.High, DateTime.Now.AddDays(1)));
            _dbContext.TodoItems.Add(TodoItem.Create("Learn Blazor", "Learn how to build a Blazor application", PriorityLevel.High, DateTime.Now.AddDays(2)));
            _dbContext.TodoItems.Add(TodoItem.Create("Learn ASP.NET Core", "Learn how to build a ASP.NET Core application", PriorityLevel.Medium, DateTime.Now.AddDays(3)));
            _dbContext.TodoItems.Add(TodoItem.Create("Learn Entity Framework Core", "Learn how to build a Entity Framework Core application", PriorityLevel.Medium, DateTime.Now.AddDays(4)));
            _dbContext.TodoItems.Add(TodoItem.Create("Learn Docker", "Learn how to build a Docker application", PriorityLevel.Low, DateTime.Now.AddDays(5)));
            _dbContext.TodoItems.Add(TodoItem.Create("Learn Kubernetes", "Learn how to build a Kubernetes application", PriorityLevel.Low, DateTime.Now.AddDays(6)));
            _dbContext.TodoItems.Add(TodoItem.Create("Learn Azure", "Learn how to build a Azure application", PriorityLevel.Low, DateTime.Now.AddDays(7)));

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while seeding the database");
            throw;
        }
    }
}