using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class ProductsSkusRepository : IRepository<ProductsSkus>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public ProductsSkusRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(ProductsSkus entity)
        {
            entity.Id = idIncrement++;
            _context.ProductsSkus.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var productsSku = _context.ProductsSkus.FirstOrDefault(c => c.Id == id);
            if (productsSku != null) _context.ProductsSkus.Remove(productsSku);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<ProductsSkus>> GetAllAsync()
        {
            return Task.FromResult(_context.ProductsSkus.AsEnumerable());
        }

        public Task<ProductsSkus?> GetByIdAsync(int id)
        {
            var productsSku = _context.ProductsSkus.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(productsSku);
        }

        public Task UpdateAsync(ProductsSkus entity)
        {
            var Index = _context.ProductsSkus.FindIndex(c => c.Id == entity.Id);
            _context.ProductsSkus[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
