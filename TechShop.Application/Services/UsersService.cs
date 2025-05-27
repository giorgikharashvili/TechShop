using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Users;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;

namespace TechShop.Application.Services
{
    public class UsersService : IEntityService<UserDto, CreateUserDto, UpdateUserDto>
    {
        private readonly UsersRepository _usersRepository;

        public UsersService(UsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            var user = new Users
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password),
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _usersRepository.AddAsync(user);
            return MapToDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _usersRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _usersRepository.GetAllAsync();
            return users.Select(MapToDto).ToList();
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException();
            return MapToDto(user);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;
            user.ModifedAt = DateTime.UtcNow;
            user.ModifiedBy = "System";

            await _usersRepository.UpdateAsync(user);
            return true;
        }

        private UserDto MapToDto(Users user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        private string HashPassword(string password)
        { 
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
        }
    }
}
