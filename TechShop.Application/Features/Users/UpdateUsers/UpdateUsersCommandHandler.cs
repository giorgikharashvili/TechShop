using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.UpdateUsers;

public class UpdateUsersCommandHandler(
    IRepository<Domain.Entities.Users> _repository,
    IMapper _mapper,
    ILogger<UpdateUsersCommandHandler> _logger
    ) : IRequestHandler<UpdateUsersCommand, bool>
{
    public async Task<bool> Handle(UpdateUsersCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling UpdateUsersCommand for User ID: {Id}", request.id);

        var users = await _repository.GetByIdAsync(request.id);
        if (users == null)
        {
            _logger.LogWarning("User with ID: {Id} not found.", request.id);
            return false;
        }

        _mapper.Map(request, users);
        _logger.LogInformation("Mapped update request to user entity.");

        users.ModifiedAt = DateTime.UtcNow;
        users.ModifiedBy = "System";

        await _repository.UpdateAsync(users);
        _logger.LogInformation("User with ID: {Id} updated successfully.", request.id);

        return true;
    }
}
