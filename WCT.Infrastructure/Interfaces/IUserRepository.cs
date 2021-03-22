using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;

namespace WCT.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(User user, string password);

        void Delete(User entity);

        Task<User> GetAsync(int userId);

        Task<IEnumerable<User>> GetAsync();

        Task<SignInResult> SignInAsync(User user, string password);

        Task<IdentityResult> ChangePasswordAsync(User user,
            string currentPassword, string newPassword);
    }
}