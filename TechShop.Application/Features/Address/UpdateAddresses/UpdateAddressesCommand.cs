using MediatR;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    

    public record UpdateAddressesCommand(
        int id,
        string AddressLine1, 
        string AddressLine2, 
        string Country, 
        string City, 
        string PostalCode
        ) : IRequest<bool>;
}
