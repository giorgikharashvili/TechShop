using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Application.Features.ProductsSkus.UpdateProductsSkus
{
    public record UpdateProductsSkusCommand(int id, UpdateProductsSkusDto Dto) : IRequest<bool>;
}
