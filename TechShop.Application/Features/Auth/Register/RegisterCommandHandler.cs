using AutoMapper;
using TechShop.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TechShop.Domain.Entities;
using TechShop.TechShop.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace TechShop.Application.Features.Auth.Register;

public class RegisterCommandHandler(
    IUserRepository _userRepository,
    IRepository<Addresses> _addressesRepository,
    IMapper _mapper,
    PasswordHasher<Domain.Entities.Users> _passwordHasher,
    ILogger<RegisterCommandHandler> _logger
   ) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling RegisterCommand for Email: {Email}", request.Dto.Email);

        var existingUser = await _userRepository.GetByEmailAsync(request.Dto.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("User with Email: {Email} already exists", request.Dto.Email);
            throw new Exception("User with given Email already exists");
        }

        var user = _mapper.Map<Domain.Entities.Users>(request.Dto);
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Dto.Password);
        user.CreatedAt = DateTime.UtcNow;

        var userId = await _userRepository.AddAsync(user);
        _logger.LogInformation("User created with ID: {UserId}", userId);

        foreach (var addresses in request.Dto.addresses)
        {
            var address = _mapper.Map<Addresses>(addresses);
            address.UserId = user.Id;
            address.CreatedBy = "User";
            address.CreatedAt = DateTime.UtcNow;

            _logger.LogInformation("Saving user's address for UserId: {UserId}", user.Id);
            await _addressesRepository.AddAsync(address);
        }

        _logger.LogInformation("User registration completed successfully for Email: {Email}", request.Dto.Email);
        return "User registered successfully";
    }
}
