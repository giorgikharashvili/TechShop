using MediatR;

namespace TechShop.Application.Features.Wishlist.UpdateWishlist
{
    public record UpdateWishlistCommand(
        int Id,
        int ProductId,
        int UserId
        ) : IRequest<bool>;
}
