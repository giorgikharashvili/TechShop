using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Cart
{
    public class CreateCartDto
    {
        [Required]
        public string UserId { get; set; }
    }
}
