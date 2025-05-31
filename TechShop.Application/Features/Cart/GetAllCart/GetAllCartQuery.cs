using MediatR;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.Application.Features.Address.GetAllCart
{
    public record GetAllCartQuery() : IRequest<IEnumerable<CartDto>>;
  
}
