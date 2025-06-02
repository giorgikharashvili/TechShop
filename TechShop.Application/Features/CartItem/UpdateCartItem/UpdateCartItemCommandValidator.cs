using FluentValidation;

namespace TechShop.Application.Features.CartItem.UpdateCartItem
{
    public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
    {
        public UpdateCartItemCommandValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductName).NotNull().Length(30);
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductIdSku).NotNull().GreaterThan(0);
        }
    }
}
