using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.Payments;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetPaymentsById
{
    public record GetPaymentsByIdQuery(int id) : IRequest<PaymentsDto?>;
}
