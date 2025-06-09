using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Cart.DeleteCart;

public class DeleteCartCommandHandler(
    IRepository<Domain.Entities.Cart> _repository,
    ILogger<DeleteCartCommandHandler> _logger
    ) : IRequestHandler<DeleteCartCommand, bool>
{
    public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteCartCommand for Cart ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("Cart with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Cart with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
