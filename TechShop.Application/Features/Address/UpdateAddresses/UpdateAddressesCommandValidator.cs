using FluentValidation;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressesCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.Dto.AddressLine1).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Dto.AddressLine2).NotEmpty().MaximumLength(50);
        }
    }
}
