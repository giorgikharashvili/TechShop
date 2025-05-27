using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class ProductSkuAttributesRepository : IRepository<ProductSkuAttributes>
    {
        private readonly shopDatabase _context;
        private static int idIncrement = 0;
        public ProductSkuAttributesRepository(shopDatabase context)
        {
            _context = context;
        }


        public Task AddAsync(ProductSkuAttributes entity)
        {
            entity.Id = idIncrement++;
            _context.ProductSkuAttributes.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var productSkuAttributes = _context.ProductSkuAttributes.FirstOrDefault(c => c.Id == id);
            if (productSkuAttributes != null) _context.ProductSkuAttributes.Remove(productSkuAttributes);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<ProductSkuAttributes>> GetAllAsync()
        {
            return Task.FromResult(_context.ProductSkuAttributes.AsEnumerable());
        }

        public Task<ProductSkuAttributes?> GetByIdAsync(int id)
        {
            var productSkuAttributes = _context.ProductSkuAttributes.FirstOrDefault(c => c.Id == id);

            return Task.FromResult(productSkuAttributes);
        }

        public Task UpdateAsync(ProductSkuAttributes entity)
        {
            var Index = _context.ProductSkuAttributes.FindIndex(c => c.Id == entity.Id);
            _context.ProductSkuAttributes[Index] = entity;

            return Task.CompletedTask;
        }
    }
}
