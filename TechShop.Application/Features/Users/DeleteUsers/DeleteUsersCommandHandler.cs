using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.DeleteUsers;

public class DeleteUsersCommandHandler(
    IRepository<Domain.Entities.Users> _repository,
    ILogger<DeleteUsersCommandHandler> _logger
    ) : IRequestHandler<DeleteUsersCommand, bool>
{
    public async Task<bool> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteUsersCommand for User ID: {Id}", request.id);

        var exists = await _repository.GetByIdAsync(request.id);
        if (exists == null)
        {
            _logger.LogWarning("User with ID: {Id} not found.", request.id);
            return false;
        }

        await _repository.DeleteAsync(request.id);
        _logger.LogInformation("User with ID: {Id} deleted successfully.", request.id);

        return true;
    }
}
