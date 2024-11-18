using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class HeroConfiguration : IEntityTypeConfiguration<Hero>
{
    public void Configure(EntityTypeBuilder<Hero> builder)
    {
        builder.HasKey(t => t.Id);

        // builder.Property(t => t.Id)
        //     .HasConversion(x => x.Value,
        //         x => new HeroId(x))
        //     .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .IsRequired();

        builder.Property(t => t.Alias)
            .IsRequired();

        // This is to highlight that we can also serialise to JSON for simple content instead of adding a new table 
        builder.OwnsMany(t => t.Powers, b => b.ToJson());
    }
}