using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.ProductsSkus
{
    public class UpdateProductsSkusDto
    {
        public int ProductId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        public string StockQuantity { get; set; }
    }
}
