using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class UsersRepository : IRepository<Users>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public UsersRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(Users entity)
        {
            entity.Id = idIncrement++;
            _context.Users.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == id);
            if (user != null) _context.Users.Remove(user);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Users>> GetAllAsync()
        {
            return Task.FromResult(_context.Users.AsEnumerable());
        }

        public Task<Users?> GetByIdAsync(int id)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(user);
        }

        public Task UpdateAsync(Users entity)
        {
            var Index = _context.Users.FindIndex(c => c.Id == entity.Id);
            _context.Users[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
