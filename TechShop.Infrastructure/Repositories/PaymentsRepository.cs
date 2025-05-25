using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class PaymentsRepository : IRepository<Payments>
    {

        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public PaymentsRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(Payments entity)
        {
            entity.Id = idIncrement++;
            _context.Payments.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var payment = _context.Payments.FirstOrDefault(c => c.Id == id);
            if (payment != null) _context.Payments.Remove(payment);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Payments>> GetAllAsync()
        {
            return Task.FromResult(_context.Payments.AsEnumerable());
        }

        public Task<Payments?> GetByIdAsync(int id)
        {
            var payment = _context.Payments.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(payment);
        }

        public Task UpdateAsync(Payments entity)
        {
            var Index = _context.Payments.FindIndex(c => c.Id == entity.Id);
            _context.Payments[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
