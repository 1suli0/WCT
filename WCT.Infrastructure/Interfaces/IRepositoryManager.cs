using System.Threading.Tasks;

namespace WCT.Infrastructure.Interfaces
{
    public interface IRepositoryManager
    {
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }

        Task SaveAsync();
    }
}