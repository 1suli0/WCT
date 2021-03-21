using System;
using System.Collections.Generic;
using System.Linq;

namespace WCT.Core
{
    public class ShoppingList
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public decimal Total
        {
            get
            {
                if (this.ShoppingListItems == null || this.ShoppingListItems.Count == 0)
                    return 0;

                return this.ShoppingListItems.Sum(i => i.Total);
            }
        }

        public ICollection<ShoppingListItem> ShoppingListItems { get; set; }
    }
}