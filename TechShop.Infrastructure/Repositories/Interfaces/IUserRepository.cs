using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
    }
}
