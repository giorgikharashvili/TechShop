using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.Entities;

namespace TechShop.Application.Services;
    public class StripeService(ProductService _productService, PriceService _priceService) : IStripeService
    {
    public string CreateCheckoutSession(List<string> priceIds, string userId)
    {
        var lineItems = priceIds.Select(priceId => new SessionLineItemOptions
        {
            Price = priceId,
            Quantity = 1
        }).ToList();

        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "https://localhost:7182/swagger/index.html",
            CancelUrl = "https://localhost:7182/swagger/index.html",
            Metadata = new Dictionary<string, string>
            {
                { "userId", userId },
            }
        };

        var service = new SessionService();
        var session = service.Create(options);

        return session.Url;
    }

    public async Task<(string ProductId, string PriceId)> CreateProductWithPriceAsync(string name, string description, long unitAmount, int productId, string lookupKey, string currency = "usd")
    {


        var product = await _productService.CreateAsync(new ProductCreateOptions
        {
            Name = name,
            Description = description,
            Metadata = new Dictionary<string, string>
        {
            { "productId", productId.ToString() },
        }
        });

        var price = await _priceService.CreateAsync(new PriceCreateOptions
        {
            UnitAmount = unitAmount,
            Currency = currency,
            Product = product.Id,
            LookupKey = lookupKey,
        });

        await _productService.UpdateAsync(product.Id, new ProductUpdateOptions
        {
            DefaultPrice = price.Id
        });

        return (product.Id, price.Id);
       }
}

