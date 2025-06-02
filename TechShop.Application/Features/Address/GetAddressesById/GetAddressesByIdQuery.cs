using MediatR;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Application.Features.Address.GetAddressesById
{
    public record GetAddressesByIdQuery(int id) : IRequest<AddressesDto?>;
}
