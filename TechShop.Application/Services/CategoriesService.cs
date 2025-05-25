using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Domain.DTOs.Categories;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Application.Services.Interfaces;

namespace TechShop.Application.Services
{
    public class CategoriesService : IEntityService<CategoriesDto, CreateCategoriesDto, UpdateCategoriesDto>
    {
        private readonly CategoriesRepository _categoriesRepository;

        public CategoriesService(CategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<CategoriesDto> CreateAsync(CreateCategoriesDto dto)
        {
            var category = new Categories
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _categoriesRepository.AddAsync(category);
            return MapToDto(category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);
            if (category == null) return false;

            await _categoriesRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<CategoriesDto>> GetAllAsync()
        {
            var categories = await _categoriesRepository.GetAllAsync();
            return categories.Select(MapToDto).ToList();
        }

        public async Task<CategoriesDto> GetByIdAsync(int id)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException();
            return MapToDto(category);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCategoriesDto dto)
        {
            var category = await _categoriesRepository.GetByIdAsync(id);
            if (category == null) return false;

            category.Name = dto.Name;
            category.Description = dto.Description;

            await _categoriesRepository.UpdateAsync(category);
            return true;
        }

        private CategoriesDto MapToDto(Categories entity)
        {
            return new CategoriesDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description
            };
        }
    }
}
