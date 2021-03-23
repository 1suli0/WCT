using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;
using WCT.Infrastructure.DBContexts;
using WCT.Infrastructure.Interfaces;

namespace WCT.Infrastructure.Repositories
{
    internal class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DBContext context) : base(context)
        {
        }

        public new Product Create(Product product)
        {
            return base.Create(product);
        }

        public async Task<bool> ExistAsync(string name, bool trackChanges)
        {
            return await base.Entity(trackChanges)
                .AnyAsync(i => i.Name == name);
        }

        public async Task<Product> GetAsync(int id, bool trackChanges)
        {
            return await base.Entity(trackChanges)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAsync(bool trackChanges)
        {
            return await base.Entity(trackChanges).ToListAsync();
        }

        public new void Delete(Product product)
        {
            base.Delete(product);
        }
    }
}