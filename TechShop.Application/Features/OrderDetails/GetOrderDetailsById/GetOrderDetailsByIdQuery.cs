using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetOrderDetailsById
{
    public record GetOrderDetailsByIdQuery(int id) : IRequest<OrderDetailsDto?>;
}
