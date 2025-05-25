using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class CategoriesRepository : IRepository<Categories>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public CategoriesRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(Categories entity)
        {
            entity.Id = idIncrement++;
            _context.Categories.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var Categories = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (Categories != null) _context.Categories.Remove(Categories);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Categories>> GetAllAsync()
        {
            return Task.FromResult(_context.Categories.AsEnumerable());
        }

        public Task<Categories?> GetByIdAsync(int id)
        {
            var Categories = _context.Categories.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(Categories);
        }

        public Task UpdateAsync(Categories entity)
        {
            var Index = _context.Categories.FindIndex(c => c.Id == entity.Id);
            _context.Categories[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
