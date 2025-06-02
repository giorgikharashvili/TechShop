using FluentValidation;

namespace TechShop.Application.Features.Cart.CreateCart
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.TotalPrice).GreaterThan(0);
        }
    }
}
