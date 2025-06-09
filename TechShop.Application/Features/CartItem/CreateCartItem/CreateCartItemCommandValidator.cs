using FluentValidation;

namespace TechShop.Application.Features.CartItem.CreateCartItem
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartItemCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Dto.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.ProductSkuId).NotNull();
        }
    }
}
