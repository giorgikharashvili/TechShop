using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes
{
    public record CreateProductsSkuAttributesCommand(
        int Id,
        string Type,
        string Name
        ) : IRequest<ProductSkuAttributesDto>;
    
}
