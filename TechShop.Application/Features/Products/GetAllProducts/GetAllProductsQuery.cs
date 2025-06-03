using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.GetAllProducts
{
    public class GetAllProductsQuery() : IRequest<IEnumerable<ProductsDto>>;
}