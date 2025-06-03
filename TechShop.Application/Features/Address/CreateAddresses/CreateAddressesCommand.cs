using MediatR;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public record CreateAddressCommand(CreateAddressesDto Dto) : IRequest<AddressesDto>;
    
}
