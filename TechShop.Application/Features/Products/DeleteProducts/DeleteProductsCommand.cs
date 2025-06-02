using MediatR;

namespace TechShop.Application.Features.Products.DeleteProducts
{
    public record DeleteProductsCommand(int id) : IRequest<bool>;



}