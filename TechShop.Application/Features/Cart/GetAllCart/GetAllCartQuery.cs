using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Cart.GetAllCart
{
    public record GetAllCartQuery() : IRequest<IEnumerable<CartDto>>;
  
}
