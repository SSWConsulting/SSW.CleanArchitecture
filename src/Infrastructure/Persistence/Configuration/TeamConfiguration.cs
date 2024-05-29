using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Teams;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    // TODO: Figure out a good marker (e.g. for recurring fields ID) to enforce that all entities have configuration defined via arch tests
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new TeamId(x))
            .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .IsRequired();

        // TODO: Check this works
        builder.HasMany(t => t.Missions)
            .WithOne()
            //.HasForeignKey(m => m.Id)
            .IsRequired();

        builder.HasMany(t => t.Heroes)
            .WithOne();
    }
}