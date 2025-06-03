using MediatR;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Application.Features.Address.UpdateAddresses
{
    

    public record UpdateAddressesCommand(int id, UpdateAddressesDto Dto) : IRequest<bool>;
}
