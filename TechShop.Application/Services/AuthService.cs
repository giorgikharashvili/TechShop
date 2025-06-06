using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services;
    public class AuthService(IUserRepository _userRepository, IConfiguration _configuration, PasswordHasher<Users> _passwordHasher) : IAuthService
    {
        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            var isPasswordValid = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (isPasswordValid != PasswordVerificationResult.Success)
                throw new UnauthorizedAccessException("Invalid email or password");

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
            };


            var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
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
