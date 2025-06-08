using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.CreateOrderDetails;

public class CreateOrderDetailsCommandHandler(
    IRepository<Domain.Entities.OrderDetails> _repository,
    IMapper _mapper,
    ILogger<CreateOrderDetailsCommandHandler> _logger
    ) : IRequestHandler<CreateOrderDetailsCommand, OrderDetailsDto>
{
    public async Task<OrderDetailsDto> Handle(CreateOrderDetailsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateOrderDetailsCommand for UserId: {UserId}", request.Dto.UserId);

        var entity = _mapper.Map<Domain.Entities.OrderDetails>(request.Dto);
        entity.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(entity);
        _logger.LogInformation("OrderDetails entity created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<OrderDetailsDto>(entity);

        return dto;
    }
}
