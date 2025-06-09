using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.OrderDetails.GetAllOrderDetails;

public class GetAllCartQueryHandler(
    IRepository<Domain.Entities.OrderDetails> _repository,
    IMapper _mapper,
    ILogger<GetAllCartQueryHandler> _logger
    ) : IRequestHandler<GetAllOrderDetailsQuery, IEnumerable<OrderDetailsDto>>
{
    public async Task<IEnumerable<OrderDetailsDto>> Handle(GetAllOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllOrderDetailsQuery");

        var orderDetails = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} order details from repository", orderDetails.Count());

        return _mapper.Map<IEnumerable<OrderDetailsDto>>(orderDetails);
    }
}
