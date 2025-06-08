using Microsoft.Extensions.Logging;
using Stripe.Checkout;
using Stripe;
using TechShop.Application.Services.Interfaces;
using TechShop.Infrastructure.Repositories.Interfaces;
using TechShop.Domain.Entities;
using System.ComponentModel.DataAnnotations;

public class StripeWebhookService(
    ILogger<StripeWebhookService> _logger,
    IPaymentProcessingService _paymentProcessingService,
    IInventoryService _inventoryService
    ) : IStripeWebhookService
{
    public async Task HandleStripeWebhookAsync(string json, string stripeSignature)
    {
        var endpointSecret = Environment.GetEnvironmentVariable("WEBHOOK_SECRET");

        if (string.IsNullOrWhiteSpace(endpointSecret))
        {
            _logger.LogError("Stripe webhook secret is not set.");
            throw new InvalidOperationException("Missing webhook secret");
        }

        Event stripeEvent;

        try
        {
            stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, endpointSecret);
        }
        catch (StripeException e)
        {
            _logger.LogError(e, "Stripe signature verification failed.");
            throw;
        }

        if (stripeEvent.Type == "checkout.session.completed")
        {
            var session = stripeEvent.Data.Object as Session;
            if (session == null)
            {
                _logger.LogWarning("Stripe session is null.");
                return;
            }

            var lineItemService = new SessionLineItemService();
            var lineItems = lineItemService.List(session.Id, new SessionLineItemListOptions
            {
                Limit = 100
            }).ToList();

            await _paymentProcessingService.ProcessCheckoutSessionAsync(session);

            await _inventoryService.UpdateStockFromLineItemsAsync(lineItems);
            
            }
        }
    }

