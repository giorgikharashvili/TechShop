using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.GetAllOrderItem;

public class GetAllOrderItemQueryHandler(
    IRepository<Domain.Entities.OrderItem> _repository,
    IMapper _mapper,
    ILogger<GetAllOrderItemQueryHandler> _logger
    ) : IRequestHandler<GetAllOrderItemQuery, IEnumerable<OrderItemDto>>
{
    public async Task<IEnumerable<OrderItemDto>> Handle(GetAllOrderItemQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllOrderItemQuery");

        var orderItem = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} order items from repository", orderItem.Count());

        return _mapper.Map<IEnumerable<OrderItemDto>>(orderItem);
    }
}
