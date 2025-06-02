using MediatR;

namespace TechShop.Application.Features.CartItem.UpdateCartItem
{
    public record UpdateCartItemCommand(
        int id,
        int ProductId,
        int ProductIdSku,
        int Quantity,
        string ProductName,
        decimal Price
        ) : IRequest<bool>;
}
