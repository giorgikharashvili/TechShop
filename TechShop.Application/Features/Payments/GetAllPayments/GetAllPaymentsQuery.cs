using MediatR;
using TechShop.Domain.DTOs.Payments;

namespace TechShop.Application.Features.Payments.GetAllPayments
{
    public record GetAllPaymentsQuery() : IRequest<IEnumerable<PaymentsDto>>;
  
}
