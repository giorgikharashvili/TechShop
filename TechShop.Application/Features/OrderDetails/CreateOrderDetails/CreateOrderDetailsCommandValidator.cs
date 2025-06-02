using FluentValidation;

namespace TechShop.Application.Features.OrderDetails.CreateOrderDetails
{
    public class CreateOrderDetailsCommandValidator : AbstractValidator<CreateOrderDetailsCommand>
    {
        public CreateOrderDetailsCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.TotalPrice).NotEmpty().GreaterThan(0);
        }
    }
}
