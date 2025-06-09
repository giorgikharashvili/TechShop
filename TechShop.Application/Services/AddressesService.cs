using AutoMapper;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Addresses;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Application.Services;

public class AddressesService(IAddressesRepository _addressesRepository, IMapper _mapper) : IAddressesService
{
    public async Task<IEnumerable<AddressesDto>> GetByCityAsync(string city)
    {
        var addressByCity = await _addressesRepository.GetByCityAsync(city);

        return _mapper.Map<IEnumerable<AddressesDto>>(addressByCity);
    }

    public async Task<IEnumerable<AddressesDto>> GetByCountryAsync(string country)
    {
        var addressByCountry = await _addressesRepository.GetByCountryAsync(country);

        return _mapper.Map<IEnumerable<AddressesDto>>(addressByCountry);
    }

    public async Task<IEnumerable<AddressesDto>> GetByPostalCodeAsync(string postalCode)
    {
        var addressByPostalCode = await _addressesRepository.GetByPostalCodeAsync(postalCode);

        return _mapper.Map<IEnumerable<AddressesDto>>(addressByPostalCode);
    }
}
