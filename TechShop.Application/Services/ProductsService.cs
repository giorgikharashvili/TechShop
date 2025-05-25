using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Products;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;

namespace TechShop.Application.Services
{
    public class ProductsService : IEntityService<ProductsDto, CreateProductDto, UpdateProductDto>
    {
        private readonly ProductsRepository _productsRepository;

        public ProductsService(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ProductsDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Products
            {
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _productsRepository.AddAsync(product);
            return MapToDto(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productsRepository.GetByIdAsync(id);
            if (product == null) return false;

            await _productsRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductsDto>> GetAllAsync()
        {
            var products = await _productsRepository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductsDto> GetByIdAsync(int id)
        {
            var product = await _productsRepository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException();

            return MapToDto(product);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _productsRepository.GetByIdAsync(id);
            if (product == null) return false;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.CategoryId = dto.CategoryId;
            product.ModifiedAt = DateTime.UtcNow;
            product.ModifiedBy = "System";

            await _productsRepository.UpdateAsync(product);
            return true;
        }

        private ProductsDto MapToDto(Products entity)
        {
            return new ProductsDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CategoryId = entity.CategoryId
            };
        }
    }
}
