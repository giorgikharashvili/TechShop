using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Cart
{
    public class UpdateCartDto
    {
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
