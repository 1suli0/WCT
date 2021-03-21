using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WCT.Core
{
    public class User : IdentityUser<int>
    {
        public ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}