using Stripe;
using TechShop.Domain.Entities;

namespace TechShop.Application.Services.Interfaces;

public interface IStripeService
{
    Task<(string ProductId, string PriceId)> CreateProductWithPriceAsync(string name, string description, long unitAmount, int productId, string lookupKey, string currency = "usd");
    string CreateCheckoutSession(List<string> priceIds, string userId);
}