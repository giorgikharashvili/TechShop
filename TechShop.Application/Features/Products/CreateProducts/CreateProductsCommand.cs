using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.CreateProducts
{
    public record CreateProductsCommand(CreateProductDto Dto) : IRequest<ProductsDto>;

}