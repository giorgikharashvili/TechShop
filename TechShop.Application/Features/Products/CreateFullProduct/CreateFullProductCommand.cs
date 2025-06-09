using MediatR;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Application.Features.Products.CreateFullProduct
{
    public record CreateFullProductCommand(FullProductDto Dto) : IRequest<int>;
}
