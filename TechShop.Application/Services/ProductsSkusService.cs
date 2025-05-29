using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services
{
    public class ProductsSkusService : IEntityService<ProductsSkusDto, CreateProductsSkusDto, UpdateProductsSkusDto>
    {
        private readonly IRepository<ProductsSkus> _repository;

        public ProductsSkusService(IRepository<ProductsSkus> repository)
        {
            _repository = repository;
        }

        public async Task<ProductsSkusDto> CreateAsync(CreateProductsSkusDto dto)
        {
            var sku = new ProductsSkus
            {
                ProductId = dto.ProductId,
                Sku = dto.Sku,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            await _repository.AddAsync(sku);
            return MapToDto(sku);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sku = await _repository.GetByIdAsync(id);
            if (sku == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<List<ProductsSkusDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<ProductsSkusDto> GetByIdAsync(int id)
        {
            var sku = await _repository.GetByIdAsync(id);
            if (sku == null) throw new KeyNotFoundException();
            return MapToDto(sku);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductsSkusDto dto)
        {
            var sku = await _repository.GetByIdAsync(id);
            if (sku == null) return false;

            sku.Price = dto.Price;
            sku.StockQuantity = dto.StockQuantity;

            await _repository.UpdateAsync(sku);
            return true;
        }

        private ProductsSkusDto MapToDto(ProductsSkus entity)
        {
            return new ProductsSkusDto
            {
                ProductId = entity.ProductId,
                Sku = entity.Sku,
                Price = entity.Price,
                StockQuantity = entity.StockQuantity
            };
        }
    }
}
