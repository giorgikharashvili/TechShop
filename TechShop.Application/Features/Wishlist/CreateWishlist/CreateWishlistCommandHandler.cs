using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Wishlist.CreateWishlist;

public class CreateWishlistCommandHandler(
    IRepository<Domain.Entities.Wishlist> _repository,
    IMapper _mapper,
    ILogger<CreateWishlistCommandHandler> _logger
    ) : IRequestHandler<CreateWishlistCommand, WishlistDto>
{
    public async Task<WishlistDto> Handle(CreateWishlistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateWishlistCommand for UserId: {UserId}", request.Dto.UserId);

        var entity = _mapper.Map<Domain.Entities.Wishlist>(request.Dto);
        entity.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(entity);
        _logger.LogInformation("Wishlist created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<WishlistDto>(entity);

        return dto;
    }
}
