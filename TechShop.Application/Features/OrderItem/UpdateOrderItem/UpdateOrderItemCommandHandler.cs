using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.UpdateOrderItem;

public class UpdateOrderItemCommandHandler(
    IRepository<Domain.Entities.OrderItem> _repository,
    IMapper _mapper,
    ILogger<UpdateOrderItemCommandHandler> _logger
    ) : IRequestHandler<UpdateOrderItemCommand, bool>
{
    public async Task<bool> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateOrderItemCommand for OrderItem ID: {Id}", request.id);

        var orderItem = await _repository.GetByIdAsync(request.id);
        if (orderItem == null)
        {
            _logger.LogWarning("OrderItem with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, orderItem);
        _logger.LogInformation("Mapped update request to OrderItem entity.");

        await _repository.UpdateAsync(orderItem);
        _logger.LogInformation("OrderItem with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
