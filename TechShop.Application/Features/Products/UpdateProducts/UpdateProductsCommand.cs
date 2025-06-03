using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.UpdateProducts
{
    public record UpdateProductsCommand(int id, UpdateProductDto Dto) : IRequest<bool>;
}