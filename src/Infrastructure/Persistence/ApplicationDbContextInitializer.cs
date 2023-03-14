using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

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
            {
                await _dbContext.Database.MigrateAsync();
            }
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
            {
                return;
            }

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn Clean Architecture",
                Note = "Learn how to build a Clean Architecture application",
                Priority = PriorityLevel.High,
                Reminder = DateTime.Now.AddDays(1),
                Done = false
            });

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn Blazor",
                Note = "Learn how to build a Blazor application",
                Priority = PriorityLevel.Medium,
                Reminder = DateTime.Now.AddDays(2),
                Done = false
            });

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn ASP.NET Core",
                Note = "Learn how to build a ASP.NET Core application",
                Priority = PriorityLevel.Low,
                Reminder = DateTime.Now.AddDays(3),
                Done = false
            });

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn Entity Framework Core",
                Note = "Learn how to build a Entity Framework Core application",
                Priority = PriorityLevel.Low,
                Reminder = DateTime.Now.AddDays(4),
                Done = false
            });

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn Docker",
                Note = "Learn how to build a Docker application",
                Priority = PriorityLevel.Low,
                Reminder = DateTime.Now.AddDays(5),
                Done = false
            });

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn Kubernetes",
                Note = "Learn how to build a Kubernetes application",
                Priority = PriorityLevel.Low,
                Reminder = DateTime.Now.AddDays(6),
                Done = false
            });

            _dbContext.TodoItems.Add(new TodoItem()
            {
                Title = "Learn Azure",
                Note = "Learn how to build a Azure application",
                Priority = PriorityLevel.Low,
                Reminder = DateTime.Now.AddDays(7),
                Done = false
            });

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while seeding the database");
            throw;
        }
    }
}