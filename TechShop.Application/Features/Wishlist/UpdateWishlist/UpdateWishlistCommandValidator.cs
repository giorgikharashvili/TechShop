using FluentValidation;
using TechShop.Application.Features.Wishlist.CreateWishlist;

namespace TechShop.Application.Features.Wishlist.UpdateWishlist
{
    public class UpdateWishlistCommandValidator : AbstractValidator<CreateWishlistCommand>
    {
        public UpdateWishlistCommandValidator()
        {
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.UserId).NotNull().GreaterThan(0);
        }
    }
}
