using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Cart.UpdateCart
{
    

    public record UpdateCartCommand(int id, UpdateCartDto Dto) : IRequest<bool>;
}
