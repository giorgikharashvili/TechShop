using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Cart.GetCartById
{
    public record GetCartByIdQuery(int id) : IRequest<CartDto?>;
}
