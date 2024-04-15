using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;
using SSW.CleanArchitecture.Domain.TodoItems;

namespace SSW.CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Hero> Heroes { get; }
    DbSet<Team> Teams { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}