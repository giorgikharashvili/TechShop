using FluentValidation;

namespace TechShop.Application.Features.CartItem.CreateCartItem
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartItemCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductName).NotNull().Length(30);
            RuleFor(x => x.Price).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductIdSku).NotNull().GreaterThan(0);
        }
    }
}
