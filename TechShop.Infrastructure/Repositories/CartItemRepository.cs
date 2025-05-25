using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class CartItemRepository : IRepository<CartItem>
    {


        private readonly shopDatabase _context;
        // again since no database to automatically increment
        private static int idIncrement = 0;

        public CartItemRepository(shopDatabase context)
        {
            _context = context;
        }

        public Task AddAsync(CartItem entity)
        {
            entity.Id = idIncrement++;
            _context.CartItems.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == id);
            if (cartItem != null) _context.CartItems.Remove(cartItem);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<CartItem>>(_context.CartItems);
        }

        public Task<CartItem?> GetByIdAsync(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(cartItem);
        }

        public Task UpdateAsync(CartItem entity)
        {
            var Index = _context.CartItems.FindIndex(c => c.Id == entity.Id);
            _context.CartItems[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
