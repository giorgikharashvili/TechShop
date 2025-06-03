using MediatR;
using TechShop.Domain.DTOs.CartItem;


namespace TechShop.Application.Features.CartItem.GetCartItemById
{
    public record GetCartByIdQuery(int id) : IRequest<CartItemDto?>;
}
