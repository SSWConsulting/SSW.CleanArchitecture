using Microsoft.EntityFrameworkCore;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Hero> Heroes { get; }
    DbSet<Team> Teams { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}