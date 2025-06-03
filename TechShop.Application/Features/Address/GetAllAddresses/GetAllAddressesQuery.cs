using MediatR;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Application.Features.Address.GetAllAddresses
{
    public record GetAllAddressQuery() : IRequest<IEnumerable<AddressesDto>>;
  
}
