using MediatR;

namespace TechShop.Application.Features.Address.DeleteAddresses
{
    public record DeleteAddressCommand(int id) : IRequest<bool>;
}
