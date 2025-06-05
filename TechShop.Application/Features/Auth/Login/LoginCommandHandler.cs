using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace TechShop.Application.Features.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Domain.Entities.Users> _passwordHasher;

        public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration, PasswordHasher<Domain.Entities.Users> passwordHasher)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null) throw new UnauthorizedAccessException("Invalid email or password");

            var isPasswordValid = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (isPasswordValid == PasswordVerificationResult.Failed) throw new UnauthorizedAccessException("Invalid email or password");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var envKey = Environment.GetEnvironmentVariable("JWT_KEY");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"]));


            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
