using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCT.Core;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class ShoppingListConfiguration : IEntityTypeConfiguration<ShoppingList>
    {
        public void Configure(EntityTypeBuilder<ShoppingList> builder)
        {
            builder.HasKey(i => i.Name);
            builder.Property(i => i.Name).ValueGeneratedNever();
            builder.Property(i => i.CreatedAt).IsRequired();
            builder.Ignore(i => i.Total);

            builder.HasMany(i => i.ShoppingListItems)
                .WithOne()
                .HasForeignKey(i => i.ShoppingListId);
        }
    }
}