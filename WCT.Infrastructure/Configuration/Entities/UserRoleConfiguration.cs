using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            builder
               .HasData(new IdentityUserRole<int>()
               {
                   RoleId = 1,
                   UserId = 1
               });
        }
    }
}