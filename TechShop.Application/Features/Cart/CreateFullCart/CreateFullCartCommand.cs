using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Cart.CreateFullCart
{
    public record CreateFullCartCommand(CreateFullCartDto Dto) : IRequest<int>;
}
