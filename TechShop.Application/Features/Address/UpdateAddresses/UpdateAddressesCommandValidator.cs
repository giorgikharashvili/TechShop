using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TechShop.Application.Features.Address.UpdateAddresses;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(x => x.id).GreaterThan(0);
            RuleFor(x => x.AddressLine1).NotEmpty().MaximumLength(50);
            RuleFor(x => x.AddressLine2).NotEmpty().MaximumLength(50);
        }
    }
}
