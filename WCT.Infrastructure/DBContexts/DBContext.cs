using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WCT.Core;
using WCT.Infrastructure.Configuration.Entities;

namespace WCT.Infrastructure.DBContexts
{
    public class DBContext : IdentityDbContext<User, Role, int>
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new ShoppingListItemConfiguration());
            builder.ApplyConfiguration(new ShoppingListConfiguration());
        }
    }
}