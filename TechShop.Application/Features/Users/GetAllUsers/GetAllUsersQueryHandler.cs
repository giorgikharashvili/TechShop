using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Users;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.GetAllUsers;

public class GetAllUsersQueryHandler(
    IRepository<Domain.Entities.Users> _repository,
    IMapper _mapper,
    ILogger<GetAllUsersQueryHandler> _logger
    ) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllUsersQuery");

        var users = await _repository.GetAllAsync();

        _logger.LogInformation("Retrieved {Count} users from repository", users.Count());

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
