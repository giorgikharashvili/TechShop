using MediatR;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.Application.Features.CartItem.CreateCartItem
{
    public record CreateCartItemCommand(
        int ProductId,
        int ProductIdSku,
        int Quantity,
        string ProductName,
        decimal Price
        ) : IRequest<CartItemDto>;
    
}
