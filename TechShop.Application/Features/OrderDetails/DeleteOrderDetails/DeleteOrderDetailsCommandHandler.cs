using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.DeleteOrderDetails;

public class DeleteCartCommandHandler(
    IRepository<Domain.Entities.OrderDetails> _repository,
    ILogger<DeleteCartCommandHandler> _logger
    ) : IRequestHandler<DeleteOrderDetailsCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteOrderDetailsCommand for OrderDetails ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("OrderDetails with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("OrderDetails with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
