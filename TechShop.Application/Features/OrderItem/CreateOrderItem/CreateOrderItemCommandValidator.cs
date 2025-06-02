using FluentValidation;

namespace TechShop.Application.Features.OrderItem.CreateOrderItem
{
    public class CreateCartCommandValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductSkuId).NotNull().GreaterThan(0);
        }
    }
}
