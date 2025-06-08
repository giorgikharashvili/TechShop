using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.UpdateWishlist;

public class UpdateWishlistCommandHandler(
    IRepository<Domain.Entities.Wishlist> _repository,
    IMapper _mapper,
    ILogger<UpdateWishlistCommandHandler> _logger
    ) : IRequestHandler<UpdateWishlistCommand, bool>
{
    public async Task<bool> Handle(UpdateWishlistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateWishlistCommand for Wishlist ID: {Id}", request.id);

        var wishlist = await _repository.GetByIdAsync(request.id);
        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, wishlist);
        _logger.LogInformation("Mapped update request to wishlist entity.");

        wishlist.ModifiedAt = DateTime.UtcNow;
        wishlist.ModifiedBy = "System";

        await _repository.UpdateAsync(wishlist);
        _logger.LogInformation("Wishlist with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
