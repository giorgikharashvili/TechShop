using FluentValidation;

namespace TechShop.Application.Features.OrderItem.CreateOrderItem
{
    public class CreateCartCommandValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.Dto.Quantity).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.ProductId).NotNull().GreaterThan(0);
        }
    }
}
