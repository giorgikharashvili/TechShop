using TechShop.Domain.DTOs.Cart;
using TechShop.Domain.DTOs.CartItem;
using TechShop.Domain.Entities;

namespace TechShop.Infrastructure.Repositories.Interfaces;

public interface ICartRepository
{
    Task<int> AddCartAsync(Cart cart);
    Task AddCartItemAsync(CartItem cartItem);
}
