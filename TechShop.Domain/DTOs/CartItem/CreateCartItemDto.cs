using System.ComponentModel;
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
        public string ProductSkuId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
