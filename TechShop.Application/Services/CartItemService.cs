using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Domain.Entities;
using TechShop.Infrastructure;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Services
{
    public class CartItemService : IEntityService<CartItemDto, CreateCartItemDto, UpdateCartItemDto>
    {
        private readonly IRepository<CartItem> _cartItemRepository;
        
        public CartItemService(IRepository<CartItem> cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItemDto> CreateAsync(CreateCartItemDto dto)
        {
            var address = new CartItem
            {
                CartId = dto.CartId,
                ProductId = dto.ProductId,
                ProductSkuId = dto.ProductSkuId,
                Quantity = dto.Quantity,
            };

            await _cartItemRepository.AddAsync(address);
            return MapToDto(address);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var address = await _cartItemRepository.GetByIdAsync(id);
            if (address == null) return false;
            await _cartItemRepository.DeleteAsync(id);

            return true;
        }

        public async Task<List<CartItemDto>> GetAllAsync()
        {
            var entities = await _cartItemRepository.GetAllAsync();
            return entities.Select(MapToDto).ToList();
        }

        public async Task<CartItemDto> GetByIdAsync(int id)
        {
            var entity = await _cartItemRepository.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException();
            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCartItemDto dto)
        {
            var existingCartItem = await _cartItemRepository.GetByIdAsync(id);
            if (existingCartItem == null) return false;

            existingCartItem.ProductSkuId = dto.ProductSkuId;
            existingCartItem.Quantity = dto.Quantity;

             await _cartItemRepository.UpdateAsync(existingCartItem);
            return true;
        }

        private CartItemDto MapToDto(CartItem entity)
        {
            return new CartItemDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                ProductSkuId = entity.ProductSkuId,
                Quantity = entity.Quantity
            };
        }
    }
}
