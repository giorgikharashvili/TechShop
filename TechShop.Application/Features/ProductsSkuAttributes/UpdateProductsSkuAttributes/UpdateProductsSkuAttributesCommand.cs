using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes
{
    public record UpdateProductsSkuAttributesCommand(int id, ProductSkuAttributesDto Dto) : IRequest<bool>;
}
