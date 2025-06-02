using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Cart.CreateCart
{
    public record CreateCartCommand(
        int id,
        decimal TotalPrice
        ) : IRequest<CartDto>;
    
}
