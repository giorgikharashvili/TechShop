using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.GetAllCartItem;

public class GetAllCartQueryHandler(
    IRepository<Domain.Entities.CartItem> _repository,
    IMapper _mapper,
    ILogger<GetAllCartQueryHandler> _logger
    ) : IRequestHandler<GetAllCartItemQuery, IEnumerable<CartItemDto>>
{
    public async Task<IEnumerable<CartItemDto>> Handle(GetAllCartItemQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllCartItemQuery");

        var entity = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} cart items from repository", entity.Count());

        return _mapper.Map<IEnumerable<CartItemDto>>(entity);
    }
}
