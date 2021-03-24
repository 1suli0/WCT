using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WCT.Core;
using WCT.Infrastructure.DBContexts;
using WCT.Infrastructure.Interfaces;

namespace WCT.Infrastructure.Repositories
{
    internal class ShoppingListRepository : BaseRepository<ShoppingList>, IShoppingListRepository
    {
        public ShoppingListRepository(DBContext context) : base(context)
        {
        }

        private new IQueryable<ShoppingList> Entity(bool trackChanges)
        {
            return base.Entity(trackChanges)
                .Include(i => i.User)
                .Include(i => i.ShoppingListItems)
                .ThenInclude(i => i.Product);
        }

        public new ShoppingList Create(ShoppingList list)
        {
            return base.Create(list);
        }

        public async Task<bool> ExistAsync(string name, int userId, bool trackChanges)
        {
            return await base.Entity(trackChanges)
                .AnyAsync(i => i.Name == name && i.UserId == userId);
        }

        public async Task<ShoppingList> GetAsync(string name, int userId, bool trackChanges)
        {
            return await this.Entity(trackChanges)
                .FirstOrDefaultAsync(i => i.Name == name && i.UserId == userId);
        }

        public async Task<IEnumerable<ShoppingList>> GetAsync(int userId, bool trackChanges)
        {
            return await base.Entity(trackChanges)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public new void Delete(ShoppingList list)
        {
            base.Delete(list);
        }
    }
}