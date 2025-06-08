using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Cart;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.GetCartById;

public class GetCartItemByIdQueryHandler(
    IRepository<Domain.Entities.Cart> _repository,
    IMapper _mapper,
    ILogger<GetCartItemByIdQueryHandler> _logger
    ) : IRequestHandler<GetCartByIdQuery, CartDto?>
{
    public async Task<CartDto?> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetCartByIdQuery for Cart ID: {Id}", request.id);

        var cart = await _repository.GetByIdAsync(request.id);
        if (cart == null)
        {
            _logger.LogWarning("Cart not found with ID: {Id}", request.id);
            return null;
        }

        _logger.LogInformation("Cart found. Mapping to CartDto.");
        return _mapper.Map<CartDto>(cart);
    }
}
