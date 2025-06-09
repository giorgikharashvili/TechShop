using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Features.Address.DeleteAddresses;

public class DeleteAddressesCommandHandler(
    IRepository<Addresses> _repository,
    ILogger<DeleteAddressesCommandHandler> _logger
    ) : IRequestHandler<DeleteAddressCommand, bool>
{
    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteAddressCommand for Address ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("Address with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("Address with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
