using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Application.Features.ProductsSkus.GetAllProductsSkus
{
    public record GetAllProductsSkusQuery() : IRequest<IEnumerable<ProductsSkusDto>>;
  
}
