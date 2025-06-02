using MediatR;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Application.Features.Payments.UpdatePayments
{
    public record UpdatePaymentsCommand(
        int id,
        int OrderId,
        int StripePaymentId,
        decimal Amount,
        string Currency,
        OrderStatus Status
        ) : IRequest<bool>;
}
