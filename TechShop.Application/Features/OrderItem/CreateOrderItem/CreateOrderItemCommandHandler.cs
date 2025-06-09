using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderItem.CreateOrderItem;

public class CreateOrderItemCommandHandler(
    IRepository<Domain.Entities.OrderItem> _repository,
    IMapper _mapper,
    ILogger<CreateOrderItemCommandHandler> _logger
    ) : IRequestHandler<CreateOrderItemCommand, OrderItemDto>
{
    public async Task<OrderItemDto> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateOrderItemCommand for ProductId: {ProductId}", request.Dto.ProductId);

        var entity = _mapper.Map<Domain.Entities.OrderItem>(request.Dto);
        await _repository.AddAsync(entity);

        _logger.LogInformation("OrderItem created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<OrderItemDto>(entity);
        
        return dto;
    }
}
