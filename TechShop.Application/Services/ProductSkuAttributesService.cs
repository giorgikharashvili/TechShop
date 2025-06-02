using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services
{
    public class ProductSkuAttributesService : IEntityService<ProductSkuAttributesDto, CreateProductSkuAttributesDto, UpdateProductsSkuAttributesDto>
    {
        private readonly IRepository<ProductSkuAttributes> _repository;

        public ProductSkuAttributesService(IRepository<ProductSkuAttributes> repository)
        {
            _repository = repository;
        }

        public async Task<ProductSkuAttributesDto> CreateAsync(CreateProductSkuAttributesDto dto)
        {
            var attribute = new ProductSkuAttributes
            {
               Id = dto.Id,
               Type = dto.Type,
               Value = dto.Value
            };

            await _repository.AddAsync(attribute);
            return MapToDto(attribute);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductSkuAttributesDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<ProductSkuAttributesDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();

            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductsSkuAttributesDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Value = dto.Value;

            await _repository.UpdateAsync(entity);
            return true;
        }

        private ProductSkuAttributesDto MapToDto(ProductSkuAttributes entity)
        {
            return new ProductSkuAttributesDto
            {
                Id = entity.Id,
                Type = entity.Type,
                Value = entity.Value
            };
        }
    }
}
