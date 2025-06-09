using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.CartItem.DeleteCartItem;

public class DeleteCartCommandHandler(
    IRepository<Domain.Entities.CartItem> _repository,
    ILogger<DeleteCartCommandHandler> _logger
    ) : IRequestHandler<DeleteCartItemCommand, bool>
{
    public async Task<bool> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteCartItemCommand for CartItem ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("CartItem with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("CartItem with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
