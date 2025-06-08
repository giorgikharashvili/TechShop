using TechShop.Domain.DTOs.Addresses;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Services.Interfaces;

public interface IAddressesService
{
    Task<IEnumerable<AddressesDto>> GetByCountryAsync(string country);
    Task<IEnumerable<AddressesDto>> GetByCityAsync(string city);
    Task<IEnumerable<AddressesDto>> GetByPostalCodeAsync(string postalCode);
}
