using Microsoft.EntityFrameworkCore;

namespace SSW.CleanArchitecture.Infrastructure.Audit;

public class AuditDbContext(DbContextOptions<AuditDbContext> options)
    : DbContext(options)
{
    public DbSet<AuditEntry> AuditEntries => Set<AuditEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var auditEntryBuilder = modelBuilder.Entity<AuditEntry>().ToTable(nameof(AuditEntry), "audit");
        auditEntryBuilder.HasKey(e => e.Id);
        auditEntryBuilder.Property(e => e.TableName).IsRequired();
        auditEntryBuilder.Property(e => e.AuditType).IsRequired();
        auditEntryBuilder.Property(e => e.AuditedAt).IsRequired();
        auditEntryBuilder.Property(e => e.UserId).IsRequired(false);
    }
}