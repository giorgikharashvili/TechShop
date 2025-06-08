using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.UpdateCartItem;

public class UpdateCartCommandHandler(
    IRepository<Domain.Entities.CartItem> _repository,
    IMapper _mapper,
    ILogger<UpdateCartCommandHandler> _logger
    ) : IRequestHandler<UpdateCartItemCommand, bool>
{
    public async Task<bool> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateCartItemCommand for CartItem ID: {Id}", request.id);

        var cartItem = await _repository.GetByIdAsync(request.id);
        if (cartItem == null)
        {
            _logger.LogWarning("CartItem with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, cartItem);
        _logger.LogInformation("Mapped update request to CartItem entity.");

        await _repository.UpdateAsync(cartItem);
        _logger.LogInformation("CartItem with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
