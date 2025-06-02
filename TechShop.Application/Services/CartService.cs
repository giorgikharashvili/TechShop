using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Domain.DTOs.Cart;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Application.Services.Interfaces;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services
{
    public class CartService : IEntityService<CartDto, CreateCartDto, UpdateCartDto>
    {
        private readonly IRepository<Cart> _cartRepository;

        public CartService(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> CreateAsync(CreateCartDto dto)
        {
            var cart = new Cart
            {
                UserId = dto.UserId,
                TotalPrice = 0m, // empty
            };

            await _cartRepository.AddAsync(cart);
            return MapToDto(cart);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null) return false;

            await _cartRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<CartDto>> GetAllAsync()
        {
            var carts = await _cartRepository.GetAllAsync();
            return carts.Select(MapToDto).ToList();
        }

        public async Task<CartDto> GetByIdAsync(int id)
        {
            var cart = await _cartRepository.GetByIdAsync(id);
            if (cart == null) throw new KeyNotFoundException();
            return MapToDto(cart);
        }

        public async Task<bool> UpdateAsync(int id, UpdateCartDto dto)
        {
            var existingCart = await _cartRepository.GetByIdAsync(id);
            if (existingCart == null) return false;

            existingCart.TotalPrice = dto.TotalPrice;
            
            await _cartRepository.UpdateAsync(existingCart);
            return true;
        }

        private CartDto MapToDto(Cart entity)
        {
            return new CartDto
            {
                Id = entity.Id,
                TotalPrice = entity.TotalPrice
            };
        }
    }
}
