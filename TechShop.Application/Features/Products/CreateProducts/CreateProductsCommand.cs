using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.CreateProducts
{
    public record CreateProductsCommand
        (
            string Name,
            string Description,
            int CategoryId
            ) : IRequest<ProductsDto>;

}