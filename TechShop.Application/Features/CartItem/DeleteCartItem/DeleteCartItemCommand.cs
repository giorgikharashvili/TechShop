using MediatR;

namespace TechShop.Application.Features.CartItem.DeleteCartItem
{
    public record DeleteCartItemCommand(int id) : IRequest<bool>;
}
