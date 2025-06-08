using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories;

public class AddressesRepository(IDbConnection _connection) : IAddressesRepository
{
    public async Task<IEnumerable<Addresses>> GetByCityAsync(string city)
    {
        var query = "SELECT * FROM [auth].[Addresses] WHERE City = @City";
        return await _connection.QueryAsync<Addresses> (query, new { City = city });
    }

    public async Task<IEnumerable<Addresses>> GetByCountryAsync(string country)
    {
        var query = "SELECT * FROM [auth].[Addresses] WHERE Country = @Country";
        return await _connection.QueryAsync<Addresses>(query, new { Country = country });
    }

    public async Task<IEnumerable<Addresses>> GetByPostalCodeAsync(string postalCode)
    {
        var query = "SELECT * FROM [auth].[Addresses] WHERE PostalCode = @PostalCode";
        return await _connection.QueryAsync<Addresses>(query, new { PostalCode = postalCode });
    }
}
