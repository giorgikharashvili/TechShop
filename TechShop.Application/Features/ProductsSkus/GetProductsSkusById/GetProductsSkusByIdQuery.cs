using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Application.Features.ProductsSkus.GetProductsSkusById
{
    public record GetProductsSkusByIdQuery(int id) : IRequest<ProductsSkusDto?>;
}
