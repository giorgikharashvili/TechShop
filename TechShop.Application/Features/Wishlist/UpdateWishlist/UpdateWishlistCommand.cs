using MediatR;
using TechShop.Domain.DTOs.Wishlist;

namespace TechShop.Application.Features.Wishlist.UpdateWishlist
{
    public record UpdateWishlistCommand(int id, UpdateWishlistDto Dto) : IRequest<bool>;
}
