using MediatR;

namespace TechShop.Application.Features.Payments.DeletePayments
{
    public record DeletePaymentsCommand(int id) : IRequest<bool>;
}
