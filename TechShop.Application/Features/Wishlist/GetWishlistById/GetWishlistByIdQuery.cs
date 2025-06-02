using MediatR;
using TechShop.Domain.DTOs.Wishlist;

namespace TechShop.Application.Features.Wishlist.GetWishlistById
{
    public record GetWishlistByIdQuery(int id) : IRequest<WishlistDto?>;
}
