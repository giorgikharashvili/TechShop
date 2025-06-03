using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes
{
    public record CreateProductsSkuAttributesCommand(CreateProductSkuAttributesDto Dto) : IRequest<ProductSkuAttributesDto>;
}
