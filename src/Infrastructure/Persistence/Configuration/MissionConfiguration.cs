using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class MissionConfiguration : IEntityTypeConfiguration<Mission>
{
    // TODO: Rip out the common pieces that are from BaseEntity (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/78)
    // virtual method, override
    // Good marker to enforce that all entities have configuration defined via arch tests
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