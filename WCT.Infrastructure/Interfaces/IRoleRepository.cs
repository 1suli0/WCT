using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;

namespace WCT.Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAsync();
    }
}