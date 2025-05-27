using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Infrastructure.Repositories
{
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public OrderDetailsRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(OrderDetails entity)
        {
            entity.Id = idIncrement++;
            _context.Orders.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(c => c.Id == id);
            if (order != null) _context.Orders.Remove(order);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<OrderDetails>> GetAllAsync()
        {
            return Task.FromResult(_context.Orders.AsEnumerable());
        }

        public Task<OrderDetails?> GetByIdAsync(int id)
        {
            var order = _context.Orders.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(order);
        }

        public Task UpdateAsync(OrderDetails entity)
        {
            var Index = _context.Orders.FindIndex(c => c.Id == entity.Id);
            _context.Orders[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
