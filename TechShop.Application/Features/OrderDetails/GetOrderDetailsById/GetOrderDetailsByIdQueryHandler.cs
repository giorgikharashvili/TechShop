using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.GetOrderDetailsById;

public class GetOrderDetailsByIdQueryHandler(
    IRepository<Domain.Entities.OrderDetails> _repository,
    IMapper _mapper,
    ILogger<GetOrderDetailsByIdQueryHandler> _logger
    ) : IRequestHandler<GetOrderDetailsByIdQuery, OrderDetailsDto?>
{
    public async Task<OrderDetailsDto?> Handle(GetOrderDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetOrderDetailsByIdQuery for OrderDetails ID: {Id}", request.id);

        var orderDetails = await _repository.GetByIdAsync(request.id);
        if (orderDetails == null)
        {
            _logger.LogWarning("OrderDetails with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("OrderDetails found. Mapping to OrderDetailsDto.");
        return _mapper.Map<OrderDetailsDto>(orderDetails);
    }
}
