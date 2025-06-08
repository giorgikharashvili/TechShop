using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.DeleteWishlist;

public class DeleteWishlistCommandHandler(
    IRepository<Domain.Entities.Wishlist> _repository,
    ILogger<DeleteWishlistCommandHandler> _logger
    ) : IRequestHandler<DeleteWishlistCommand, bool>
{
    public async Task<bool> Handle(DeleteWishlistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteWishlistCommand for Wishlist ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("Wishlist with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Wishlist with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
