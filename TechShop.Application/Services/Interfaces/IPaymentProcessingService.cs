using Stripe.Checkout;
using TechShop.Domain.Entities;

namespace TechShop.Application.Services.Interfaces
{
    public interface IPaymentProcessingService
    {
        Task<OrderDetails> ProcessCheckoutSessionAsync(Session session);
    }
}
