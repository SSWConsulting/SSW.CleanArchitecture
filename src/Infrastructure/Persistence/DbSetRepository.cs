using Ardalis.Specification.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public class DbSetRepository<T> : RepositoryBase<T> where T : class
{
    public DbSetRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}