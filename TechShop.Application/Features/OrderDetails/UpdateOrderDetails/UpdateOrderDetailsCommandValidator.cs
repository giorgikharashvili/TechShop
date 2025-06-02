using FluentValidation;
using TechShop.Application.Features.OrderDetails.CreateOrderDetails;

namespace TechShop.Application.Features.OrderDetails.UpdateOrderDetails
{
    public class UpdateOrderDetailsCommandValidator : AbstractValidator<CreateOrderDetailsCommand>
    {
        public UpdateOrderDetailsCommandValidator()
        {
            RuleFor(x => x.UserId).NotNull().GreaterThan(0);
            RuleFor(x => x.TotalPrice).NotNull().GreaterThan(0);
        }
    }
}
