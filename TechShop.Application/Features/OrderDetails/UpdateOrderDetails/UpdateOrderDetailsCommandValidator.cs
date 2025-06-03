using FluentValidation;
using TechShop.Application.Features.OrderDetails.CreateOrderDetails;

namespace TechShop.Application.Features.OrderDetails.UpdateOrderDetails
{
    public class UpdateOrderDetailsCommandValidator : AbstractValidator<CreateOrderDetailsCommand>
    {
        public UpdateOrderDetailsCommandValidator()
        {
            RuleFor(x => x.Dto.UserId).NotNull().GreaterThan(0);
            RuleFor(x => x.Dto.TotalPrice).NotNull().GreaterThan(0);
        }
    }
}
