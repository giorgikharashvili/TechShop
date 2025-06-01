using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.OrderDetails;

namespace TechShop.Application.Features.Address.GetAllOrderDetails
{
    public record GetAllOrderDetailsQuery() : IRequest<IEnumerable<OrderDetailsDto>>;
  
}
