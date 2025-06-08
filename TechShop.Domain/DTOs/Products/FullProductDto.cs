using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Domain.DTOs.Products
{
    public class FullProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<ProductsSkusDto> Skus { get; set; }
    }

}
