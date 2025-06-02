using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.GetProductsById
{
    public record GetProductsByIdQuery(int id) : IRequest<ProductsDto>;
}