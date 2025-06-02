using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Application.Features.ProductsSkus.CreateProductsSkus
{
    public record CreateProductsSkusCommand(
        int ProductId,
        decimal Price,
        string Sku,
        string StockQuantity
        ) : IRequest<ProductsSkusDto>;
    
}
