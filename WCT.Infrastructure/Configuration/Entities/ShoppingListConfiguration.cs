using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCT.Core;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Ignore(i => i.Total);

            builder.HasIndex(i => i.Name).IsUnique();
            builder.HasMany(i => i.ShoppingListItems)
                .WithOne()
                .HasForeignKey(i => i.ShoppingListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}