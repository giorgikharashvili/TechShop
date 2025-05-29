using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TechShop.Domain.Attributes;
using TechShop.Infrastructure.Data;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public GenericRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));  
            var values = string.Join(", ", props.Select(p => "@" + p.Name));

            var query = $"INSERT INTO {FullTableName()} ({columns}) VALUES ({values})";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }

        public Task DeleteAsync(int id)
        {
            var query = $"DELETE FROM {FullTableName()} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {FullTableName()}";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<T>(query);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM {FullTableName()} WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task UpdateAsync(T entity)
        {
            var props = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
            var setClause = string.Join(", ", props.Select(p => $"[{p.Name}] = @{p.Name}"));

            var query = $"UPDATE {FullTableName()} SET {setClause} WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, entity);
        }

        private string GetSchema()
        {
            var type = typeof(T);
            var attr = (DbSchemaAttribute)Attribute.GetCustomAttribute(type, typeof(DbSchemaAttribute));
            return attr?.Schema; 
        }

        private string FullTableName()
        {
            return $"[{GetSchema()}].[{typeof(T).Name}]";
        }
    }
}
