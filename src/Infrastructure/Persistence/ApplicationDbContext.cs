using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Common.Interfaces;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;
using SSW.CleanArchitecture.Domain.TodoItems;
using SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace SSW.CleanArchitecture.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions options,
    EntitySaveChangesInterceptor saveChangesInterceptor,
    DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Hero> Heroes => AggregateRootSet<Hero>();

    public DbSet<Team> Teams => AggregateRootSet<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Order of the interceptors is important
        optionsBuilder.AddInterceptors(saveChangesInterceptor, dispatchDomainEventsInterceptor);
    }

    private DbSet<T> AggregateRootSet<T>() where T : class, IAggregateRoot => Set<T>();
}