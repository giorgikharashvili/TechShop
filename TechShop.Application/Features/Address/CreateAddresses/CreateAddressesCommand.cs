using MediatR;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Application.Features.Address.CreateAddresses
{
    public record CreateAddressCommand(
        string AddressLine1,
        string AddressLine2,
        string Country,
        string City,
        string PostalCode,
        int UserId
        ) : IRequest<AddressesDto>;
    
}
