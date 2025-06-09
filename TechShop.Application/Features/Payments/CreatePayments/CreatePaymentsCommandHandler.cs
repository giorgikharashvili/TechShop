using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Payments;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.CreatePayments;

public class CreatePaymentsCommandHandler(
    IRepository<Domain.Entities.Payments> _repository,
    IMapper _mapper,
    ILogger<CreatePaymentsCommandHandler> _logger
    ) : IRequestHandler<CreatePaymentsCommand, PaymentsDto>
{
    public async Task<PaymentsDto> Handle(CreatePaymentsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreatePaymentsCommand for OrderId: {OrderId}", request.Dto.OrderId);

        var entity = _mapper.Map<Domain.Entities.Payments>(request.Dto);
        entity.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(entity);
        _logger.LogInformation("Payment created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<PaymentsDto>(entity);

        return dto;
    }
}
