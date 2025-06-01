using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.Application.Features.Address.GetAllCartItem
{
    public record GetAllCartItemQuery() : IRequest<IEnumerable<CartItemDto>>;
  
}
