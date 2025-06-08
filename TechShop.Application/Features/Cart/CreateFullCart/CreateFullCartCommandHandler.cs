using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.CreateFullCart;

public class CreateFullCartCommandHandler(
    ICartRepository _cartRepository,
    IMapper _mapper,
    ILogger<CreateFullCartCommandHandler> _logger
    ) : IRequestHandler<CreateFullCartCommand, int>
{
    public async Task<int> Handle(CreateFullCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateFullCartCommand for UserId: {UserId}", request.Dto.UserId);

        var cart = _mapper.Map<Domain.Entities.Cart>(request.Dto);
        var newCartId = await _cartRepository.AddCartAsync(cart);

        _logger.LogInformation("Cart created with ID: {CartId} for UserId: {UserId}", newCartId, request.Dto.UserId);

        foreach (var Items in request.Dto.Items)
        {
            var cartItems = _mapper.Map<Domain.Entities.CartItem>(Items);
            cartItems.CartId = newCartId;

            await _cartRepository.AddCartItemAsync(cartItems);
            _logger.LogInformation("Added CartItem to CartId: {CartId}", newCartId);
        }

        return cart.Id;
    }
}
