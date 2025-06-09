using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.UpdateCart;

public class UpdateCartCommandHandler(
    IRepository<Domain.Entities.Cart> _repository,
    IMapper _mapper,
    ILogger<UpdateCartCommandHandler> _logger
    ) : IRequestHandler<UpdateCartCommand, bool>
{
    public async Task<bool> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateCartCommand for Cart ID: {Id}", request.id);

        var cart = await _repository.GetByIdAsync(request.id);
        if (cart == null)
        {
            _logger.LogWarning("Cart with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request.Dto, cart);
        cart.Id = request.id;
        _logger.LogInformation("Mapped update request to existing cart entity.");
        

        await _repository.UpdateAsync(cart);
        _logger.LogInformation("Cart with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
