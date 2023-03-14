using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Configuration;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    // TODO: Rip out the common pieces that are from BaseEntity
    // virtual method, override 
    // Good marker to enforce that all entities have configuration defined via arch tests
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