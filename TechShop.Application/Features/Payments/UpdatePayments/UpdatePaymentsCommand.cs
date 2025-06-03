using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Application.Features.Payments.UpdatePayments
{
    public record UpdatePaymentsCommand(int id, UpdatePaymentStatusDto Dto) : IRequest<bool>;
}
