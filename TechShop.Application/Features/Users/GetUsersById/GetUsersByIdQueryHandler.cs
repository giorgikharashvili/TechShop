using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Users;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.GetUsersById;

public class GetUsersByIdQueryHandler(
    IRepository<Domain.Entities.Users> _repository,
    IMapper _mapper,
    ILogger<GetUsersByIdQueryHandler> _logger
    ) : IRequestHandler<GetUsersByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetUsersByIdQuery for User ID: {Id}", request.id);

        var users = await _repository.GetByIdAsync(request.id);
        if (users == null)
        {
            _logger.LogWarning("User with ID: {Id} not found.", request.id);
            return null;
        }

        _logger.LogInformation("User found. Mapping to UserDto.");
        return _mapper.Map<UserDto>(users);
    }
}
