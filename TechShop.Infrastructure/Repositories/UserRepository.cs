using System.Data;
using Dapper;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        public UserRepository(IDbConnection connection) : base(connection)
        {
            
        }
        public Task<Users?> GetByEmailAsync(string email)
        {
            string sqlQuery = "SELECT * FROM [auth].[Users] WHERE Email = @Email";
            return _connection.QueryFirstOrDefaultAsync<Users>(sqlQuery, new { Email = email });
        }

    }
}
