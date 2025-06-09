using Microsoft.AspNetCore.Mvc;
using Stripe;
using TechShop.Application.Services.Interfaces;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/webhooks/stripe")]
public class StripeWebhookController(IStripeWebhookService _stripeWebHookService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Handle()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var stripeSignature = Request.Headers["Stripe-Signature"];

        await _stripeWebHookService.HandleStripeWebhookAsync(json, stripeSignature);

        return Ok();
    }
}