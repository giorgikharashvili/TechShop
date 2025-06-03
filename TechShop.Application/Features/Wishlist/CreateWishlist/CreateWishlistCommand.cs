using MediatR;
using TechShop.Domain.DTOs.Wishlist;

namespace TechShop.Application.Features.Wishlist.CreateWishlist
{
    public record CreateWishlistCommand(CreateWishlistDto Dto) : IRequest<WishlistDto>;
    
}
