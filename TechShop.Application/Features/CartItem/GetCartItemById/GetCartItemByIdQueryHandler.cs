using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.GetCartItemById;

public class GetCartItemByIdQueryHandler(
    IRepository<Domain.Entities.CartItem> _repository,
    IMapper _mapper,
    ILogger<GetCartItemByIdQueryHandler> _logger
    ) : IRequestHandler<GetCartByIdQuery, CartItemDto?>
{
    public async Task<CartItemDto?> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetCartByIdQuery for CartItem ID: {Id}", request.id);

        var address = await _repository.GetByIdAsync(request.id);
        if (address == null)
        {
            _logger.LogWarning("CartItem with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("CartItem found. Mapping to CartItemDto.");
        return _mapper.Map<CartItemDto>(address);
    }
}
