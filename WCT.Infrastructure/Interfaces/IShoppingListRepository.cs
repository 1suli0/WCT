using System.Collections.Generic;
using System.Threading.Tasks;
using WCT.Core;

namespace WCT.Infrastructure.Interfaces
{
    public interface IShoppingListRepository
    {
        public ShoppingList Create(ShoppingList list);

        public Task<bool> ExistAsync(string name, int userId, bool trackChanges = false);

        public Task<ShoppingList> GetAsync(string name, int userId, bool trackChanges = true);

        public Task<IEnumerable<ShoppingList>> GetAsync(int userId, bool trackChanges = false);

        public void Delete(ShoppingList list);
    }
}