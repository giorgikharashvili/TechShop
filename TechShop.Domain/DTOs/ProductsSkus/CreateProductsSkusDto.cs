namespace TechShop.Domain.DTOs.ProductsSkus
{
    public class CreateProductsSkusDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public string StockQuantity { get; set; }
    }
}
