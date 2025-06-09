using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.GetOrderItemById;

public class GetOrderItemByIdQueryHandler(
    IRepository<Domain.Entities.OrderItem> _repository,
    IMapper _mapper,
    ILogger<GetOrderItemByIdQueryHandler> _logger
    ) : IRequestHandler<GetOrderItemByIdQuery, OrderItemDto?>
{
    public async Task<OrderItemDto?> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetOrderItemByIdQuery for OrderItem ID: {Id}", request.id);

        var orderItem = await _repository.GetByIdAsync(request.id);
        if (orderItem == null)
        {
            _logger.LogWarning("OrderItem with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("OrderItem found. Mapping to OrderItemDto.");
        return _mapper.Map<OrderItemDto>(orderItem);
    }
}
