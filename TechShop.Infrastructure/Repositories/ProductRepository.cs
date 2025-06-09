
using System;
using System.Data;
using Dapper;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories;
    public class ProductRepository(IDbConnection _connection) : IProductRepository
    {
        public async Task<int> AddAsync(Products product)
        {
            var props = typeof(Products).GetProperties().Where(p => p.Name != "Id").ToList();
            var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));
            var values = string.Join(", ", props.Select(p => "@" + p.Name));

            var query = $@" INSERT INTO [catalog].[Products] ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() AS INT);";

             var newlySetId = await _connection.ExecuteScalarAsync<int>(query, product);
             product.GetType().GetProperty("Id")?.SetValue(product, newlySetId); 

             return newlySetId;
        }

        public async Task AddAttributeAsync(ProductSkuAttributes attribute)
        {
            var props = typeof(ProductSkuAttributes).GetProperties().Where(p => p.Name != "Id").ToList();
            var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));
            var values = string.Join(", ", props.Select(p => "@" + p.Name));

            var query = $@"INSERT INTO [catalog].[ProductSkuAttributes] ({columns}) VALUES ({values});";

            await _connection.ExecuteAsync(query, attribute);
        }

        public async Task<int> AddSkuAsync(ProductsSkus sku)
        {
            var props = typeof(ProductsSkus).GetProperties().Where(p => p.Name != "Id").ToList();
            var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));
            var values = string.Join(", ", props.Select(p => "@" + p.Name));

            var query = $@"INSERT INTO [catalog].[ProductsSkus] ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() AS INT);";

            return await _connection.ExecuteScalarAsync<int>(query, sku);
        }

        public async Task<IEnumerable<Products>> GetByCategoryId(int categoryId)
        {
            var query = "SELECT * FROM [catalog].[Products] WHERE CategoryId = @CategoryId";
            return await _connection.QueryAsync<Products>(query, new { CategoryId = categoryId });
        }
}

