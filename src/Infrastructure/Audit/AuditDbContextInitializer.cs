using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SSW.CleanArchitecture.Infrastructure.Audit;

public class AuditDbContextInitializer(
    ILogger<AuditDbContextInitializer> logger,
    AuditDbContext dbContext)
{
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
}