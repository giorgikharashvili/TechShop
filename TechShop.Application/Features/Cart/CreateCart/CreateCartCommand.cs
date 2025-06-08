using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Cart.CreateCart
{
    public record CreateCartCommand(CreateCartDto Dto) : IRequest<CartDto>;
    
}
