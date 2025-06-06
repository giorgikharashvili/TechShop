using AutoMapper;
using TechShop.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TechShop.Domain.Entities;


namespace TechShop.Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<Domain.Entities.Users> _passwordHasher;

        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, PasswordHasher<Domain.Entities.Users> passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Dto.Email);
            if (existingUser != null) throw new Exception("User with given Email already exists");

            var user = _mapper.Map<Domain.Entities.Users>(request.Dto);

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Dto.Password);
            user.CreatedAt = DateTime.UtcNow;

            await _userRepository.AddAsync(user);

            return "User registered successfully";
        }
    }
}
