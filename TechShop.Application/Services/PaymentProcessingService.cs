using Stripe;
using Stripe.Checkout;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.Entities;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Application.Services;

public class PaymentProcessingService(
    IOrderDetailsRepository _orderDetailsRepository,
    IRepository<Payments> _paymentsRepository,
    IRepository<OrderItem> _orderItemRepository) : IPaymentProcessingService
{
    public async Task<OrderDetails> ProcessCheckoutSessionAsync(Session session)
    {
        var userId = int.TryParse(session.Metadata["userId"], out var id) ? id : 0;

        var order = new OrderDetails
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "StripeWebhook",
            Email = session.CustomerDetails?.Email ?? "unknown@user.com",
            TotalPrice = (int)((session.AmountTotal ?? 0) / 100)
        };

        var orderId = await _orderDetailsRepository.AddAsync(order);
        order.Id = orderId;

        var sessionService = new SessionService();
        var fullSession = await sessionService.GetAsync(session.Id, new SessionGetOptions
        {
            Expand = new List<string>
        {
        "line_items",
        "line_items.data.price.product"
        }
        });

        foreach (var item in fullSession.LineItems.Data)
        {
            var price = item.Price;

            if (price?.Product is not Product product)
                throw new Exception("Price.Product is null or not expanded. Ensure 'line_items.data.price.product' is expanded.");

            if (!product.Metadata.TryGetValue("productId", out var productIdStr))
                throw new KeyNotFoundException("Metadata key 'productId' not found in product metadata.");


            int productId = int.Parse(productIdStr);
            int quantity = (int)(item.Quantity ?? 1);

            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity
            };

            await _orderItemRepository.AddAsync(orderItem);
        }

        var payment = new Payments
        {
            OrderId = order.Id,
            StripeSessionId = session.Id,
            StripePaymentId = session.PaymentIntentId,
            Amount = (decimal)((session.AmountTotal ?? 0) / 100m),
            Currency = session.Currency,
            Status = OrderStatus.Paid,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "StripeWebhook"
        };

        await _paymentsRepository.AddAsync(payment);

        return order;
    }
}
