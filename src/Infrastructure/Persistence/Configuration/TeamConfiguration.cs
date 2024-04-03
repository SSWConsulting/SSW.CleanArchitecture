using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Heroes;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    // TODO: Rip out the common pieces that are from BaseEntity (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/78)
    // virtual method, override
    // Good marker to enforce that all entities have configuration defined via arch tests
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new TeamId(x))
            .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(Constants.DefaultNameMaxLength);

        // TODO: Check this works
        builder.HasMany(t => t.Missions)
            .WithOne()
            //.HasForeignKey(m => m.Id)
            .IsRequired();

        // TODO: Check this works
        builder.HasMany(t => t.Heroes)
            .WithOne()
            //.HasForeignKey(m => m.Id)
            .IsRequired();
    }
}