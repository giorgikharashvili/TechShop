using FluentValidation;

namespace TechShop.Application.Features.Payments.UpdatePayments
{
    public class UpdatePaymentsCommandValidator :  AbstractValidator<UpdatePaymentsCommand>
    {
        public UpdatePaymentsCommandValidator()
        {
            RuleFor(x => x.OrderId).NotNull().GreaterThan(0);
            RuleFor(x => x.Amount).NotNull().GreaterThan(0);
        }
    }
}
