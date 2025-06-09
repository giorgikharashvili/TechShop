
using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<int> AddAsync(Products product);
        Task <IEnumerable<Products>> GetByCategoryId(int CategoryId);
        Task<int> AddSkuAsync(ProductsSkus sku);
        Task AddAttributeAsync(ProductSkuAttributes attribute);
    }
}
