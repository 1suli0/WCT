using System;
using System.Linq;
using WCT.Core;
using WCT.Infrastructure.DTOs.Input;
using WCT.Infrastructure.DTOs.Output;

namespace WCT.Infrastructure.Utilities.Mapping
{
    public static class ShoppingListMapper
    {
        public static ShoppingList Map(InShoppingListDTO shoppingListDTO, int userId)
        {
            return new ShoppingList
            {
                Name = shoppingListDTO.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                ShoppingListItems = shoppingListDTO.ShoppingListItems
                .Select(shoppingListItem => Map(shoppingListItem)).ToList()
            };
        }

        public static ShoppingListItem Map(InShoppingListItemDTO shoppingListItemDTO)
        {
            return new ShoppingListItem
            {
                Quantity = shoppingListItemDTO.Quantity,
                Price = shoppingListItemDTO.Price,
                ProductId = shoppingListItemDTO.ProductId
            };
        }

        public static OutShoppingListDTO Map(ShoppingList shoppingList)
        {
            return new OutShoppingListDTO
            {
                Name = shoppingList.Name,
                Total = shoppingList.Total,
                CreatedAt = shoppingList.CreatedAt,
                User = new OutUserDTO
                {
                    Id = shoppingList.User.Id,
                    Email = shoppingList.User.Email
                },
                ShoppingListItems = shoppingList.ShoppingListItems
                .Select(shoppingListItem => new OutShoppingListItemDTO
                {
                    Id = shoppingListItem.Id,
                    ProductName = shoppingListItem.Product.Name,
                    Quantity = shoppingListItem.Quantity,
                    Price = shoppingListItem.Price,
                    Total = shoppingListItem.Total
                })
            };
        }
    }
}