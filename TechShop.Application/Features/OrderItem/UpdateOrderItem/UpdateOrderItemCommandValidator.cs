using FluentValidation;

namespace TechShop.Application.Features.OrderItem.UpdateOrderItem
{
    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(x => x.Dto.Quantity).NotNull().GreaterThan(0);
        }
    }
}
