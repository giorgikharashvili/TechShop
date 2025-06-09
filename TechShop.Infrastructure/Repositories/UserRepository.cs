using System.Data;
using Dapper;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories;

public class UserRepository(IDbConnection _connection) : IUserRepository
{
    
    public async Task<int> AddAsync(Users user)
    {
        var query = @"INSERT INTO [auth].[Users] (FirstName, LastName, Username, Email, PasswordHash, PhoneNumber, Role, CreatedAt, CreatedBy) VALUES (@FirstName, @LastName, @Username, @Email, @PasswordHash, @PhoneNumber, @Role, @CreatedAt, @CreatedBy);SELECT CAST(SCOPE_IDENTITY() AS INT);";
        var id = await _connection.ExecuteScalarAsync<int>(query, user);
        user.Id = id;
        return id;
    }

    public async Task<Users?> GetByEmailAsync(string email)
    {
        string query = "SELECT * FROM [auth].[Users] WHERE Email = @Email";
        return await _connection.QueryFirstOrDefaultAsync<Users>(query, new { Email = email });
    }

}
