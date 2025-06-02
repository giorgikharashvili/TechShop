using FluentValidation;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(50);
            RuleFor(x => x.AddressLine2).NotEmpty().MaximumLength(50);
        }
    }
}
