using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;

namespace WCT.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        public Product Create(Product product);

        public Task<bool> ExistAsync(string name, bool trackChanges = false);

        public Task<Product> GetAsync(int id, bool trackChanges = true);

        public Task<IEnumerable<Product>> GetAsync(bool trackChanges = false);

        public void Delete(Product product);
    }
}