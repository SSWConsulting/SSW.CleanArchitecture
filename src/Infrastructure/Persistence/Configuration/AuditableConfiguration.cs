using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Common.Base;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

// DM: Add integration tests for property string lengths
// DM: Test this all works
public abstract class AuditableConfiguration<T> : IEntityTypeConfiguration<T>
    where T : Auditable
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.CreatedBy)
            .HasMaxLength(Auditable.CreatedByMaxLength)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(Auditable.UpdatedByMaxLength);
    }
}