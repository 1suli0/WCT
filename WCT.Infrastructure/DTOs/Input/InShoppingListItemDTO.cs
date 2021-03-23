using System.ComponentModel.DataAnnotations;

namespace WCT.Infrastructure.DTOs.Input
{
    public class InShoppingListItemDTO
    {
        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}