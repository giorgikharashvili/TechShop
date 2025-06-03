using MediatR;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.Application.Features.CartItem.CreateCartItem
{
    public record CreateCartItemCommand(CreateCartItemDto Dto) : IRequest<CartItemDto>;
    
}
