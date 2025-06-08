using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.DeleteOrderItem;

public class DeleteOrderItemCommandHandler(
    IRepository<Domain.Entities.OrderItem> _repository,
    ILogger<DeleteOrderItemCommandHandler> _logger
    ) : IRequestHandler<DeleteOrderItemCommand, bool>
{
    public async Task<bool> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteOrderItemCommand for OrderItem ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("OrderItem with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("OrderItem with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
