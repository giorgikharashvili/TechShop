using System;
using System.Data;
using Dapper;
using TechShop.Domain.DTOs.Cart;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;

namespace TechShop.Infrastructure.Repositories;

public class CartRepository(IDbConnection _connection) : ICartRepository
{
    public async Task<int> AddCartAsync(Cart cart)
    {

        var props = typeof(Cart).GetProperties().Where(p => p.Name != "Id").ToList();
        var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));
        var values = string.Join(", ", props.Select(p => "@" + p.Name));

        var query = $@"INSERT INTO [cart].[Cart] ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() AS INT);";

        var newId = await _connection.ExecuteScalarAsync<int>(query, cart);
        typeof(Cart).GetProperty("Id")?.SetValue(cart, newId);

        return newId;
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        var props = typeof(CartItem).GetProperties().Where(p => p.Name != "Id").ToList();
        var columns = string.Join(", ", props.Select(p => $"[{p.Name}]"));
        var values = string.Join(", ", props.Select(p => "@" + p.Name));

        var query = $@"INSERT INTO [cart].[CartItem] ({columns}) VALUES ({values});";

        await _connection.ExecuteAsync(query, cartItem);
    }
}
