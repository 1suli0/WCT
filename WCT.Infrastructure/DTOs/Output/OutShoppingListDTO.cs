using System;
using System.Collections.Generic;

namespace WCT.Infrastructure.DTOs.Output
{
    public class OutShoppingListDTO
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public OutUserDTO User { get; set; }
        public IEnumerable<OutShoppingListItemDTO> ShoppingListItems { get; set; }
    }
}