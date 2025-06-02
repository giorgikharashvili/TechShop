using MediatR;

namespace TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes
{
    public record UpdateProductsSkuAttributesCommand(
        int Id,
        string Type,
        string Name
        ) : IRequest<bool>;
}
