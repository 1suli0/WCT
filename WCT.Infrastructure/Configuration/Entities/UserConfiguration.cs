using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCT.Core;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(i => i.ShoppingLists)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .IsRequired();

            builder.HasData(
                new User()
                {
                    Id = 1,
                    UserName = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedEmail = "ADMIN@TEST.COM",
                    EmailConfirmed = false,
                    PasswordHash = new PasswordHasher<User>().HashPassword(null, "test"),
                    SecurityStamp = string.Empty,
                });
        }
    }
}