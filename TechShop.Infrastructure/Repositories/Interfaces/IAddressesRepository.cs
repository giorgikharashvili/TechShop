
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces
{
    public interface IAddressesRepository
    {
        Task<IEnumerable<Addresses>> GetByCountryAsync(string country);
        Task<IEnumerable<Addresses>> GetByCityAsync(string city);
        Task<IEnumerable<Addresses>> GetByPostalCodeAsync(string postalCode);
    }
}
