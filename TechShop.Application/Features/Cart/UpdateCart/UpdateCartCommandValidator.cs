using FluentValidation;
using TechShop.Application.Features.Wishlist.CreateWishlist;

namespace TechShop.Application.Features.Cart.UpdateCart
{
    public class UpdateCartCommandValidator :  AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.id).NotNull();
            RuleFor(x => x.TotalPrice).GreaterThan(0);
        }
    }
}
