using FluentValidation;

namespace TechShop.Application.Features.Wishlist.CreateWishlist
{
    public class CreateWishlistCommandValidator  : AbstractValidator<CreateWishlistCommand>
    {
        public CreateWishlistCommandValidator()
        {
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.UserId).NotNull().GreaterThan(0);
        }
    }
}
