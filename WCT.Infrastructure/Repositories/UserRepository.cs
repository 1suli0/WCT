using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;
using WCT.Infrastructure.DBContexts;
using WCT.Infrastructure.Interfaces;

namespace WCT.Infrastructure.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        internal UserRepository(UserManager<User> userManager,
            SignInManager<User> signInManager, DBContext context) : base(context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public new void Delete(User user)
        {
            base.Delete(user);
        }

        public async Task<User> GetAsync(int userId)
        {
            return await this._userManager
                .Users.FirstOrDefaultAsync(i => i.Id == userId);
        }

        public async Task<User> GetAsync(string email)
        {
            return await this._userManager
                .FindByEmailAsync(email);
        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await this._userManager
                .Users.ToListAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user,
            string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword,
                newPassword);
        }

        public async Task<SignInResult> SignInAsync(User user, string password)
        {
            return await this._signInManager
                .PasswordSignInAsync(user, password, false, false);
        }

        public async Task<IEnumerable<string>> GetRolesForUserAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}