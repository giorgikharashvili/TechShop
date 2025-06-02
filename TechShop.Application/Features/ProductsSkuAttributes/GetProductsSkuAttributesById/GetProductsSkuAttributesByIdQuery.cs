using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.ProductsSkuAttributes.GetProductsSkuAttributesById
{
    public record GetProductsSkuAttributesByIdQuery(int id) : IRequest<ProductSkuAttributesDto?>;
}
