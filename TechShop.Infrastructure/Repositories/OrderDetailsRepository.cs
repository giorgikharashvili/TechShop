using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories;

public class OrderDetailsRepository(IDbConnection _connection) : IOrderDetailsRepository
{
    public async Task<int> AddAsync(OrderDetails order)
    {
        var query = @"INSERT INTO [orders].[OrderDetails] (UserId, TotalPrice, CreatedAt, Email) VALUES (@UserId, @TotalPrice, @CreatedAt, @Email); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        var newlySetId = await _connection.ExecuteScalarAsync<int>(query, order);
        order.Id = newlySetId;
        return newlySetId;
    }
}
