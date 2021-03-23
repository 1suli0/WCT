using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WCT.Infrastructure.DTOs.Input
{
    public class InShoppingListDTO
    {
        [Required]
        public string Name { get; set; }

        //Empty collection if list is without items
        [Required]
        public ICollection<InShoppingListItemDTO> ShoppingListItems { get; set; }
    }
}