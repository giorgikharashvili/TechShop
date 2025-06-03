using FluentValidation;
using TechShop.Application.Features.Wishlist.CreateWishlist;

namespace TechShop.Application.Features.Cart.UpdateCart
{
    public class UpdateCartCommandValidator :  AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.Dto.TotalPrice).GreaterThan(0);
        }
    }
}
