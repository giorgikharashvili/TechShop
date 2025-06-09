using System.Data;
using Dapper;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories;

public class ProductSkuRepository(IDbConnection _connection) : IProductSkuRepository
{
    public async Task DecreaseStockAsync(string sku, int quantity)
    {
        var sql = @"UPDATE catalog.ProductsSkus SET StockQuantity = CAST(CAST(StockQuantity AS INT) - @Qty AS VARCHAR) WHERE Sku = @Sku";
        await _connection.ExecuteAsync(sql, new { Sku = sku, Qty = quantity });
    }

    public async Task<ProductsSkus?> GetBySkuAsync(string sku)
    {
        var query = "SELECT TOP 1 * FROM catalog.ProductsSkus WHERE Sku = @Sku";
        return await _connection.QueryFirstOrDefaultAsync<ProductsSkus>(query, new { Sku = sku });
    }

    public async Task UpdateAsync(ProductsSkus sku)
    {
        var query = @"UPDATE catalog.ProductsSkus SET Price = @Price, StockQuantity = @StockQuantity WHERE Id = @Id";
        await _connection.ExecuteAsync(query, sku);
    }
}
