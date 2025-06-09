using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.UpdatePayments;

public class UpdatePaymentsCommandHandler(
    IRepository<Domain.Entities.Payments> _repository,
    IMapper _mapper,
    ILogger<UpdatePaymentsCommandHandler> _logger
    ) : IRequestHandler<UpdatePaymentsCommand, bool>
{
    public async Task<bool> Handle(UpdatePaymentsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdatePaymentsCommand for Payment ID: {Id}", request.id);

        var payments = await _repository.GetByIdAsync(request.id);
        if (payments == null)
        {
            _logger.LogWarning("Payment with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request.Dto, payments);
        _logger.LogInformation("Mapped update request to Payment entity.");

        payments.ModifiedAt = DateTime.UtcNow;
        payments.ModifiedBy = "System";

        await _repository.UpdateAsync(payments);
        _logger.LogInformation("Payment with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
