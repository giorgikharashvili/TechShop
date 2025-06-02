using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Application.Features.Payments.CreatePayments
{
    public record CreatePaymentsCommand(
       int OrderId,
       int StripePaymentId,
       decimal Amount,
       string Currency,
       OrderStatus Status
        ) : IRequest<PaymentsDto>;
    
}
