using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;
using WCT.Infrastructure.Interfaces;

namespace WCT.Infrastructure.Repositories
{
    internal class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager;

        internal RoleRepository(RoleManager<Role> roleManager)
        {
            this._roleManager = roleManager;
        }

        public async Task<IEnumerable<Role>> GetAsync()
        {
            return await this._roleManager.Roles.ToListAsync();
        }
    }
}