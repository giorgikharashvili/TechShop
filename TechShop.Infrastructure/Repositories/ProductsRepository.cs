using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class ProductsRepository : IRepository<Products>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public ProductsRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(Products entity)
        {
            entity.Id = idIncrement++;
            _context.Products.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product != null) _context.Products.Remove(product);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Products>> GetAllAsync()
        {
            return Task.FromResult(_context.Products.AsEnumerable());
        }

        public Task<Products?> GetByIdAsync(int id)
        {
            var product = _context.Products.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(product);
        }

        public Task UpdateAsync(Products entity)
        {
            var Index = _context.Products.FindIndex(c => c.Id == entity.Id);
            _context.Products[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
