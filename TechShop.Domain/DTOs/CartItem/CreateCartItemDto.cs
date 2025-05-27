using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.CartItem
{
    public class CreateCartItemDto
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductSkuId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
