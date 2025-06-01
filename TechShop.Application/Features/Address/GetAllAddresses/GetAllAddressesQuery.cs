using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Application.Features.Address.GetAllAddresses
{
    public record GetAllCartItemQuery() : IRequest<IEnumerable<AddressesDto>>;
  
}
