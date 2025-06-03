using MediatR;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.Application.Features.CartItem.UpdateCartItem
{
    public record UpdateCartItemCommand(int id,UpdateCartItemDto Dto) : IRequest<bool>;
}
