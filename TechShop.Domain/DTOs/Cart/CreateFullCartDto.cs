using System.ComponentModel.DataAnnotations;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.Domain.DTOs.Cart
{
    public class CreateFullCartDto
    {
        [Required]
        public int UserId { get; set; }

        public int TotalPrice { get; set; }

        public List<CreateFullCartItemDto> Items { get; set; }
    }
}
