using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.GetAddressesById;

public class GetAddressByIdQueryHandler(
    IRepository<Addresses> _repository,
    IMapper _mapper,
    ILogger<GetAddressByIdQueryHandler> _logger
    ) : IRequestHandler<GetAddressesByIdQuery, AddressesDto?>
{
    public async Task<AddressesDto?> Handle(GetAddressesByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAddressesByIdQuery for ID: {Id}", request.id);

        var address = await _repository.GetByIdAsync(request.id);
        if (address == null)
        {
            _logger.LogWarning("Address not found with ID: {Id}", request.id);
            return null;
        }

        _logger.LogInformation("Address found. Mapping to AddressesDto.");
        return _mapper.Map<AddressesDto>(address);
    }
}
