using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.CartItem
{
    public class UpdateCartItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductSkuId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
