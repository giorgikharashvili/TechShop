using MediatR;
using TechShop.Domain.DTOs.Wishlist;

namespace TechShop.Application.Features.Wishlist.CreateWishlist
{
    public record CreateWishlistCommand(
       int ProductId,
       int UserId
        ) : IRequest<WishlistDto>;
    
}
