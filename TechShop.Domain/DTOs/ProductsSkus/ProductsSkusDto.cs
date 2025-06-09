using System.ComponentModel.DataAnnotations;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Domain.DTOs.ProductsSkus
{
    public class ProductsSkusDto
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        public string StockQuantity { get; set; }
        public List<ProductSkuAttributesDto> Attributes { get; set; }
    }
}
