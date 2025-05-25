using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public CartRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(Cart entity)
        {
            entity.Id = idIncrement++;
            _context.Carts.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart != null) _context.Carts.Remove(cart);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Cart>> GetAllAsync()
        {
            return Task.FromResult(_context.Carts.AsEnumerable());
        }

        public Task<Cart?> GetByIdAsync(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(cart);
        }

        public Task UpdateAsync(Cart entity)
        {
            var Index = _context.Carts.FindIndex(c => c.Id == entity.Id);
            _context.Carts[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
