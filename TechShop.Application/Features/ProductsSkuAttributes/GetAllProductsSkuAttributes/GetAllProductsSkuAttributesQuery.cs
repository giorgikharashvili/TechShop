using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.ProductsSkuAttributes.GetAllProductsSkuAttributes
{
    public record GetAllProductsSkuAttributesQuery() : IRequest<IEnumerable<ProductSkuAttributesDto>>;
  
}
