using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TechShop.Application.Features.Address.DeleteAddresses
{
    public record DeleteAddressCommand(int id) : IRequest<bool>;
}
