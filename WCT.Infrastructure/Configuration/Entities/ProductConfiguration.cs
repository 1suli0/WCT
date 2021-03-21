using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCT.Core;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired();
            builder.Property(s => s.Price)
                .HasColumnType("decimal(18,2)").IsRequired();

            builder.HasMany(i => i.ShoppingListItems)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            builder.HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 2.5M
                },
                new Product()
                {
                    Id = 2,
                    Name = "Banana",
                    Price = 4
                },
                new Product()
                {
                    Id = 3,
                    Name = "Cranberries",
                    Price = 3.5M
                },
                new Product()
                {
                    Id = 4,
                    Name = "Lemon",
                    Price = 3M
                },
                new Product()
                {
                    Id = 5,
                    Name = "Pineapple",
                    Price = 5.2M
                });
        }
    }
}