using System.ComponentModel.DataAnnotations;

namespace WCT.Infrastructure.DTOs.Input
{
    public class InSignInDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}