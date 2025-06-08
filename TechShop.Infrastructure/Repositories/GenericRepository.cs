using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TechShop.Domain.Attributes;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly IDbConnection _connection;

    public GenericRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task AddAsync(T entity)
    {
        var props = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
        var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));  
        var values = string.Join(", ", props.Select(p => "@" + p.Name));

        var query = $"INSERT INTO {FullTableName()} ({columns}) VALUES ({values})";

       
        await _connection.ExecuteAsync(query, entity);
    }

    public Task DeleteAsync(int id)
    {
        var query = $"DELETE FROM {FullTableName()} WHERE Id = @Id";
       
        return _connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = $"SELECT * FROM {FullTableName()}";
        return await _connection.QueryAsync<T>(query);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var query = $"SELECT * FROM {FullTableName()} WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
    }

    public async Task UpdateAsync(T entity)
    {
        var props = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
        var setClause = string.Join(", ", props.Select(p => $"[{p.Name}] = @{p.Name}"));
        var query = $"UPDATE {FullTableName()} SET {setClause} WHERE Id = @Id";
        await _connection.ExecuteAsync(query, entity);
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
