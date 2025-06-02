using FluentValidation;

namespace TechShop.Application.Features.OrderItem.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductId).NotNull().GreaterThan(0);
            RuleFor(x => x.ProductSkuId).NotNull().GreaterThan(0);
        }
    }
}
