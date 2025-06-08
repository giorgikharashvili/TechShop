using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.ProductsSkuAttributes
{
    public class ProductSkuAttributesDto
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
