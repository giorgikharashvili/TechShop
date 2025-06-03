using MediatR;
using TechShop.Domain.DTOs.Wishlist;

namespace TechShop.Application.Features.Wishlist.GetAllWishlist
{
    public record GetAllWishlistQuery() : IRequest<IEnumerable<WishlistDto>>;
  
}
