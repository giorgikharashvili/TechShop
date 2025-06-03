using MediatR;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.Application.Features.CartItem.GetAllCartItem
{
    public record GetAllCartItemQuery() : IRequest<IEnumerable<CartItemDto>>;
  
}
