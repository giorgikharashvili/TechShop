using MediatR;

namespace TechShop.Application.Features.ProductsSkus.UpdateProductsSkus
{
    public record UpdateProductsSkusCommand(
        int Id,
        int ProductId,
        decimal Price,
        string Sku,
        string StockQuantity
        ) : IRequest<bool>;
}
