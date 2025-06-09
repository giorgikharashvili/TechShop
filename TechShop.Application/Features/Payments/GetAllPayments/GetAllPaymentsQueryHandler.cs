using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Payments;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.GetAllPayments;

public class GetAllPaymentsQueryHandler(
    IRepository<Domain.Entities.Payments> _repository,
    IMapper _mapper,
    ILogger<GetAllPaymentsQueryHandler> _logger
    ) : IRequestHandler<GetAllPaymentsQuery, IEnumerable<PaymentsDto>>
{
    public async Task<IEnumerable<PaymentsDto>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllPaymentsQuery");

        var payments = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} payments from repository", payments.Count());

        return _mapper.Map<IEnumerable<PaymentsDto>>(payments);
    }
}
