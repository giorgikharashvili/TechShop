using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.UpdateAddresses;

public class UpdateAddressCommandHandler(
    IRepository<Addresses> _repository,
    IMapper _mapper,
    ILogger<UpdateAddressCommandHandler> _logger
    ) : IRequestHandler<UpdateAddressesCommand, bool>
{
    public async Task<bool> Handle(UpdateAddressesCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateAddressesCommand for Address ID: {Id}", request.id);

        var address = await _repository.GetByIdAsync(request.id);
        if (address == null)
        {
            _logger.LogWarning("Address with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, address);
        _logger.LogInformation("Mapped update request to existing address entity.");

        address.ModifiedAt = DateTime.UtcNow;
        address.ModifiedBy = "System";

        await _repository.UpdateAsync(address);
        _logger.LogInformation("Address with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
