using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCT.Core;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class ShoppingListItemConfiguration : IEntityTypeConfiguration<ShoppingListItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingListItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Quantity).IsRequired();
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Ignore(i => i.Total);
        }
    }
}