using FluentValidation;
using TechShop.Application.Features.Wishlist.CreateWishlist;

namespace TechShop.Application.Features.Wishlist.UpdateWishlist
{
    public class UpdateWishlistCommandValidator : AbstractValidator<CreateWishlistCommand>
    {
        public UpdateWishlistCommandValidator()
        {
            RuleFor(x => x.Dto.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.UserId).NotNull();
        }
    }
}
