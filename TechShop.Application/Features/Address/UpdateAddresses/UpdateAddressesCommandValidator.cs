using FluentValidation;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressesCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
            RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(50);
            RuleFor(x => x.AddressLine2).NotEmpty().MaximumLength(50);
        }
    }
}
