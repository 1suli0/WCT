using System.Collections.Generic;

namespace WCT.Core
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}