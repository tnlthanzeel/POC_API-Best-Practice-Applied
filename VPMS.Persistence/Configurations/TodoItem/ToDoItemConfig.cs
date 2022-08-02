using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VPMS.Persistence.Configurations.TodoItem;

internal class ToDoItemConfig : IEntityTypeConfiguration<Domain.Entities.TodoItemEntities.TodoItem>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.TodoItemEntities.TodoItem> builder)
    {
        builder.HasKey(k => k.Id);

        builder.HasIndex(f => f.IsDeleted);
        builder.HasQueryFilter(f => f.IsDeleted == false);

        builder.Property(p => p.Title).IsRequired().HasMaxLength(250);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
    }
}
