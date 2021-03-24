using System;
using System.Collections.Generic;
using System.Linq;
using WCT.Core;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.Utilities.Mapping;

namespace WCT.Infrastructure.Extensions
{
    public static class Model
    {
        public static void Update(this Product product, InProductDTO productDTO)
        {
            product.Name = productDTO.Name;
            product.Price = productDTO.Price;
        }

        public static void Update(this ShoppingList shoppingList, InShoppingListDTO shoppingListDTO)
        {
            shoppingList.Name = shoppingListDTO.Name;
            shoppingList.CreatedAt = DateTime.UtcNow;
            shoppingList.ShoppingListItems = new List<ShoppingListItem>(
                shoppingListDTO.ShoppingListItems
                .Select(i => ShoppingListMapper.Map(i)).ToList());
        }
    }
}