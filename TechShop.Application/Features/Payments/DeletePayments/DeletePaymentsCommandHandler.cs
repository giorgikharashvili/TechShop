using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Payments.DeletePayments;

public class DeletePaymentsCommandHandler(
    IRepository<Domain.Entities.Payments> _repository,
    ILogger<DeletePaymentsCommandHandler> _logger
    ) : IRequestHandler<DeletePaymentsCommand, bool>
{
    public async Task<bool> Handle(DeletePaymentsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeletePaymentsCommand for Payment ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("Payment with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Payment with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
