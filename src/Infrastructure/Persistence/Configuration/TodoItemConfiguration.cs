using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSW.CleanArchitecture.Domain.Entities;

namespace SSW.CleanArchitecture.Infrastructure.Persistence.Configuration;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
            .HasConversion(x => x.Value,
                x => new TodoItemId(x))
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}