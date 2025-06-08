using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.GetWishlistById;

public class GetWishlistByIdQueryHandler(
    IRepository<Domain.Entities.Wishlist> _repository,
    IMapper _mapper,
    ILogger<GetWishlistByIdQueryHandler> _logger
    ) : IRequestHandler<GetWishlistByIdQuery, WishlistDto?>
{
    public async Task<WishlistDto?> Handle(GetWishlistByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetWishlistByIdQuery for Wishlist ID: {Id}", request.id);

        var wishlist = await _repository.GetByIdAsync(request.id);
        if (wishlist == null)
        {
            _logger.LogWarning("Wishlist with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("Wishlist found. Mapping to WishlistDto.");
        return _mapper.Map<WishlistDto>(wishlist);
    }
}
