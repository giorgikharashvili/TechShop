using MediatR;

namespace TechShop.Application.Features.Wishlist.DeleteWishlist
{
    public record DeleteWishlistCommand(int id) : IRequest<bool>;
}
