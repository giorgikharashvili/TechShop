using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Payments;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.GetPaymentsById;

public class GetPaymentsByIdQueryHandler(
    IRepository<Domain.Entities.Payments> _repository,
    IMapper _mapper,
    ILogger<GetPaymentsByIdQueryHandler> _logger
    ) : IRequestHandler<GetPaymentsByIdQuery, PaymentsDto?>
{
    public async Task<PaymentsDto?> Handle(GetPaymentsByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetPaymentsByIdQuery for Payment ID: {Id}", request.id);

        var payments = await _repository.GetByIdAsync(request.id);
        if (payments == null)
        {
            _logger.LogWarning("Payment with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("Payment found. Mapping to PaymentsDto.");
        return _mapper.Map<PaymentsDto>(payments);
    }
}
