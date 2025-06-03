using FluentValidation;

namespace TechShop.Application.Features.CartItem.UpdateCartItem
{
    public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
    {
        public UpdateCartItemCommandValidator()
        {
            RuleFor(x => x.Dto.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.ProductId).NotNull().GreaterThan(0);
        }
    }
}
