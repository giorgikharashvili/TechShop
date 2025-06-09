using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.CreateAddresses;

public class CreateAddressesCommandHandler(
    IRepository<Addresses> _repository,
    IMapper _mapper,
    ILogger<CreateAddressesCommandHandler> _logger
    ) : IRequestHandler<CreateAddressCommand, AddressesDto>
{
    public async Task<AddressesDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateAddressCommand for user: {UserId}", request.Dto.UserId);

        var entity = _mapper.Map<Addresses>(request.Dto);
        entity.CreatedAt = DateTime.UtcNow;

        _logger.LogInformation("Mapped AddressesDto to Addresses entity.");

        await _repository.AddAsync(entity);
        _logger.LogInformation("Address entity added to repository with ID: {Id}", entity.Id);

        var dto = _mapper.Map<AddressesDto>(entity);
        _logger.LogInformation("Returning created AddressesDto.");

        return dto;
    }
}
