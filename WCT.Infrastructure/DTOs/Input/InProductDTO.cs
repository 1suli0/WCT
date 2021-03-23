using System.ComponentModel.DataAnnotations;

namespace WCT.Infrastructure.DTOs.Input
{
    public class InProductDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}