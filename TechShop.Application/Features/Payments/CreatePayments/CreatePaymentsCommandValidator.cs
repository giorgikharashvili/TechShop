using FluentValidation;

namespace TechShop.Application.Features.Payments.CreatePayments
{
    public class CreatePaymentsCommandValidator :  AbstractValidator<CreatePaymentsCommand>
    {
        public CreatePaymentsCommandValidator()
        {
            RuleFor(x => x.OrderId).NotNull().GreaterThan(0);
            RuleFor(x => x.Amount).NotNull().GreaterThan(0);
        }
    }
}
