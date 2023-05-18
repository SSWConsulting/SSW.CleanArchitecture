using Ardalis.Specification.EntityFrameworkCore;

namespace SSW.CleanArchitecture.Infrastructure.Persistence;

public class DbSetRepository<T> : RepositoryBase<T> where T : class
{
    public DbSetRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}