using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class WishlistRepository : IRepository<Wishlist>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public WishlistRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(Wishlist entity)
        {
            entity.Id = idIncrement++;
            _context.Wishlists.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(c => c.Id == id);
            if (wishlist != null) _context.Wishlists.Remove(wishlist);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Wishlist>> GetAllAsync()
        {
            return Task.FromResult(_context.Wishlists.AsEnumerable());
        }

        public Task<Wishlist?> GetByIdAsync(int id)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(wishlist);
        }

        public Task UpdateAsync(Wishlist entity)
        {
            var Index = _context.Wishlists.FindIndex(c => c.Id == entity.Id);
            _context.Wishlists[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
