using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.CartItem
{
    public class UpdateCartItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductSkuId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
