using GeneratedEntityFramework;
using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Application.Common.Interfaces;
using SSW.CleanArchitecture.Domain.TodoItems;
using SSW.CleanArchitecture.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace SSW.CleanArchitecture.Infrastructure.Persistence;

[GeneratedDbContext]
public partial class ApplicationDbContext(
    DbContextOptions options,
    EntitySaveChangesInterceptor saveChangesInterceptor,
    DispatchDomainEventsInterceptor dispatchDomainEventsInterceptor)
    : DbContext(options), IApplicationDbContext
{
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
}