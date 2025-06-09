using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddAsync(Users users);
        Task<Users?> GetByEmailAsync(string email);
    }
}
