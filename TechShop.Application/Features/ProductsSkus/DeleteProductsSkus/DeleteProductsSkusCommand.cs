using MediatR;

namespace TechShop.Application.Features.ProductsSkus.DeleteProductsSkus
{
    public record DeleteProductsSkusCommand(int id) : IRequest<bool>;
}
