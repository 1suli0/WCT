using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WCT.Core;

namespace WCT.Infrastructure.Configuration.Entities
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
              new Role()
              {
                  Id = 1,
                  Name = "Administrator",
                  NormalizedName = "ADMINISTRATOR"
              },
               new Role()
               {
                   Id = 2,
                   Name = "Customer",
                   NormalizedName = "CUSTOMER"
               });
        }
    }
}