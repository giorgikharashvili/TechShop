using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public OrderItemRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(OrderItem entity)
        {
            entity.Id = idIncrement++;
            _context.OrderItems.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(c => c.Id == id);
            if (orderItem != null) _context.OrderItems.Remove(orderItem);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return Task.FromResult(_context.OrderItems.AsEnumerable());
        }

        public Task<OrderItem?> GetByIdAsync(int id)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(orderItem);
        }

        public Task UpdateAsync(OrderItem entity)
        {
            var Index = _context.OrderItems.FindIndex(c => c.Id == entity.Id);
            _context.OrderItems[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
