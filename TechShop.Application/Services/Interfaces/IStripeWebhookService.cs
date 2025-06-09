
namespace TechShop.Application.Services.Interfaces
{
    public interface IStripeWebhookService
    {
        Task HandleStripeWebhookAsync(string json, string stripeSignature);
    }
}
