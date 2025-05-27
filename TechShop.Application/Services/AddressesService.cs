using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.DTOs;
using TechShop.Infrastructure.Repositories;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Addresses;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Services
{
    public class AddressesService : IEntityService<AddressesDto, CreateAddressesDto, UpdateAddressesDto>
    {
        private readonly AddressesRepository _addressesRepository;

        public AddressesService(AddressesRepository addressesRepository)
        {
            _addressesRepository = addressesRepository;
        }

        public async Task<AddressesDto> CreateAsync(CreateAddressesDto dto)
        {
            var address = new Addresses
            {
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                Country = dto.Country,
                City = dto.City,
                PostalCode = dto.PostalCode,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _addressesRepository.AddAsync(address);
            return MapToDto(address);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var address = await _addressesRepository.GetByIdAsync(id);
            if (address == null) return false;
            await _addressesRepository.DeleteAsync(id);

            return true;
        }

        public async Task<List<AddressesDto>> GetAllAsync()
        {
            var entities = await _addressesRepository.GetAllAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<AddressesDto> GetByIdAsync(int id)
        {
            var entity = await _addressesRepository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();
            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateAddressesDto dto)
        {
            var existingAddress = await _addressesRepository.GetByIdAsync(id);
            if (existingAddress == null) return false;

            existingAddress.AddressLine1 = dto.AddressLine1;
            existingAddress.AddressLine2 = dto.AddressLine2;
            existingAddress.Country = dto.Country;
            existingAddress.City = dto.City;
            existingAddress.PostalCode = dto.PostalCode;
            existingAddress.ModifiedAt = DateTime.UtcNow;
            existingAddress.ModifiedBy = "System"; 

            await _addressesRepository.UpdateAsync(existingAddress);
            return true;
        }

        private AddressesDto MapToDto(Addresses entity)
        {
            return new AddressesDto
            {
                Id = entity.Id,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                Country = entity.Country,
                City = entity.City,
                PostalCode = entity.PostalCode
            };
        }
    }
}
