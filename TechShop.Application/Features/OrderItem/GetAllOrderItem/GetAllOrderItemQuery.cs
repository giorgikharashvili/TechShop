using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.OrderItem;

namespace TechShop.Application.Features.Address.GetAllOrderItem
{
    public record GetAllOrderItemQuery() : IRequest<IEnumerable<OrderItemDto>>;
  
}
