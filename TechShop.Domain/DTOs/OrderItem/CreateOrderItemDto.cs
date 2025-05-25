using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.OrderItem
{
    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ProductsSkuId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
