using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Payments;

namespace TechShop.Application.Features.Address.GetAllPayments
{
    public record GetAllPaymentsQuery() : IRequest<IEnumerable<PaymentsDto>>;
  
}
