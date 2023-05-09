using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;
    private readonly DispatchDomainEventsInterceptor _dispatchDomainEventsInterceptor;

    public ApplicationDbContext(
        DbContextOptions options,
        EntitySaveChangesInterceptor saveChangesInterceptor,
        DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
        : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
        _dispatchDomainEventsInterceptor = dispatchDomainEventsInterceptor;
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Order of the interceptors is important
        optionsBuilder.AddInterceptors(_saveChangesInterceptor, _dispatchDomainEventsInterceptor);
    }
}