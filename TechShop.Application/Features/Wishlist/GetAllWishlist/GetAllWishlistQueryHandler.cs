using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.GetAllWishlist;

public class GetAllWishlistQueryHandler(
    IRepository<Domain.Entities.Wishlist> _repository,
    IMapper _mapper,
    ILogger<GetAllWishlistQueryHandler> _logger
    ) : IRequestHandler<GetAllWishlistQuery, IEnumerable<WishlistDto>>
{
    public async Task<IEnumerable<WishlistDto>> Handle(GetAllWishlistQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllWishlistQuery");

        var wishlist = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} wishlist items from repository", wishlist.Count());

        return _mapper.Map<IEnumerable<WishlistDto>>(wishlist);
    }
}
