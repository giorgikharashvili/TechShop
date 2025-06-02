using MediatR;

namespace TechShop.Application.Features.ProductsSkuAttributes.DeleteProductsSkuAttributes
{
    public record DeleteProductsSkuAttributesCommand(int id) : IRequest<bool>;
}
