using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAllAddresses;

public class GetAllAddressQueryHandler(
    IRepository<Addresses> _repository,
    IMapper _mapper,
    ILogger<GetAllAddressQueryHandler> _logger
    ) : IRequestHandler<GetAllAddressQuery, IEnumerable<AddressesDto>>
{
    public async Task<IEnumerable<AddressesDto>> Handle(GetAllAddressQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllAddressQuery");

        var addresses = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} addresses from repository", addresses.Count());

        return _mapper.Map<IEnumerable<AddressesDto>>(addresses);
    }
}
