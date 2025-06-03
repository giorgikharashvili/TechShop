using MediatR;
using TechShop.Domain.DTOs.Payments;


namespace TechShop.Application.Features.Payments.GetPaymentsById
{
    public record GetPaymentsByIdQuery(int id) : IRequest<PaymentsDto?>;
}
