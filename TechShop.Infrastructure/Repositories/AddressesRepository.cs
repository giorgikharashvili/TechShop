using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.Domain.Entities;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories
{
    public class AddressesRepository : IRepository<Addresses>
    {
        private readonly shopDatabase _context;

        public AddressesRepository(shopDatabase context)
        {
            _context = context;
        }

        public Task AddAsync(Addresses entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            var address = _context.Addresses.FirstOrDefault(a => a.Id == id);
            if (address != null) _context.Addresses.Remove(address);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Addresses>> GetAllAsync()
        {
            return Task.FromResult(_context.Addresses.AsEnumerable());
        }

        public Task<Addresses?> GetByIdAsync(int id)
        {
            var address = _context.Addresses.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(address);
        }

        public Task UpdateAsync(Addresses entity)
        {
            var index = _context.Addresses.FindIndex(a => a.Id == entity.Id);
            _context.Addresses[index] = entity;

            return Task.CompletedTask;
        }
    }
}
