using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TechShop.Domain.DTOs.Users;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Features.Users.CreateUsers;

public class CreateUsersCommandHandler(
    IRepository<Domain.Entities.Users> _repository,
    IMapper _mapper,
    PasswordHasher<Domain.Entities.Users> _passwordHasher,
    ILogger<CreateUsersCommandHandler> _logger
    ) : IRequestHandler<CreateUsersCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateUsersCommand for email: {Email}", request.Dto.Email);

        var entity = _mapper.Map<Domain.Entities.Users>(request.Dto);
        entity.CreatedAt = DateTime.UtcNow;
        entity.PasswordHash = _passwordHasher.HashPassword(entity, request.Dto.Password);

        await _repository.AddAsync(entity);
        _logger.LogInformation("User created with ID: {Id}", entity.Id);

        var dto = _mapper.Map<UserDto>(entity);

        return dto;
    }
}
