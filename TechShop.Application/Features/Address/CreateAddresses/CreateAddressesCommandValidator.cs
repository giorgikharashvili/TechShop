using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public class CreateCartCommandValidator : AbstractValidator<CreateAddressesCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(50);
            RuleFor(x => x.AddressLine2).NotEmpty().MaximumLength(50);
        }
    }
}
