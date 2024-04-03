using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Common;
using SSW.CleanArchitecture.Domain.Heroes;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class HeroConfiguration : IEntityTypeConfiguration<Hero>
{
    // TODO: Rip out the common pieces that are from BaseEntity (https://github.com/SSWConsulting/SSW.CleanArchitecture/issues/78)
    // virtual method, override 
    // Good marker to enforce that all entities have configuration defined via arch tests
    public void Configure(EntityTypeBuilder<Hero> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new HeroId(x))
            .ValueGeneratedNever();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(Constants.DefaultNameMaxLength);

        builder.Property(t => t.Alias)
            .IsRequired()
            .HasMaxLength(Constants.DefaultNameMaxLength);

        builder.OwnsMany(t => t.Powers, b => b.ToJson());
    }
}