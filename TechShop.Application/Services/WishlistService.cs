using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Application.Services
{
    public class WishlistService : IEntityService<WishlistDto, CreateWishlistDto, UpdateWishlistDto>
    {
        private readonly IRepository<Wishlist> _wishlistRepository;

        public WishlistService(IRepository<Wishlist> wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<WishlistDto> CreateAsync(CreateWishlistDto dto)
        {
            var wishlistItem = new Wishlist
            {
                ProductId = dto.ProductId,
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = dto.UserId
            };

            await _wishlistRepository.AddAsync(wishlistItem);
            return MapToDto(wishlistItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _wishlistRepository.GetByIdAsync(id);
            if (item == null) return false;

            await _wishlistRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<WishlistDto>> GetAllAsync()
        {
            var items = await _wishlistRepository.GetAllAsync();
            return items.Select(MapToDto).ToList();
        }

        public async Task<WishlistDto> GetByIdAsync(int id)
        {
            var item = await _wishlistRepository.GetByIdAsync(id);
            if (item == null) throw new KeyNotFoundException();

            return MapToDto(item);
        }

        public async Task<bool> UpdateAsync(int id, UpdateWishlistDto dto)
        {
            var item = await _wishlistRepository.GetByIdAsync(id);
            if (item == null) return false;

            item.ProductId = dto.ProductId;
            item.ModifiedAt = DateTime.UtcNow;
            item.ModifiedBy = "System";

            await _wishlistRepository.UpdateAsync(item);
            return true;
        }

        private WishlistDto MapToDto(Wishlist entity)
        {
            return new WishlistDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
