using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Application.Features.ProductsSkus.CreateProductsSkus
{
    public record CreateProductsSkusCommand(CreateProductsSkusDto Dto) : IRequest<ProductsSkusDto>;   
}
