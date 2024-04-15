using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    // TODO: Figure out a good marker (e.g. for recurring fields ID) to enforce that all entities have configuration defined via arch tests
    public void Configure(EntityTypeBuilder<Mission> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new MissionId(x))
            .ValueGeneratedNever();

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(Constants.DefaultDescriptionMaxLength);
    }
}