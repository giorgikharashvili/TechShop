using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces
{
    public interface IProductSkuRepository
    {
        Task DecreaseStockAsync(string sku, int quantity);
        Task<ProductsSkus?> GetBySkuAsync(string sku);
        Task UpdateAsync(ProductsSkus sku);
    }
}
