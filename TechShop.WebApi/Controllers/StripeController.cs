using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Stripe;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.Constants;

namespace TechShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StripeController(ProductService _productService, IStripeService _stripeService, ILogger<StripeController> _logger) : ControllerBase
{

    /// <summary>
    /// To create a checkout session
    /// u first need the products stripe default price id
    /// 1. get all products from stripe GetAllProducts Endpoint
    /// 2. search for your product
    /// 3. copy the default price id and paste it 
    /// default_price : { id: " " }
    /// </summary>
    /// <param name="priceIds">stripe default price id of the product</param>
    /// <returns></returns>
    [HttpPost("Post")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Manager}, {UserRoles.Admin}")]
    public IActionResult Pay([FromBody] List<string> priceIds)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0";
        _logger.LogInformation("Creating the stripe checkout url with PriceId: {priceIds} and UserId: {userId}", priceIds, userId);
        var checkoutUrl =  _stripeService.CreateCheckoutSession(priceIds, userId);

        _logger.LogInformation("Created the stripe checkout url successfully");
        return Ok(checkoutUrl);
    }

    [HttpGet("GetAllProducts")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public IActionResult GetAllProducts()
    {
        var options = new ProductListOptions { Expand = new List<string>() { "data.default_price" } };

        var products = _productService.List(options);

        return Ok(products);
    }
}
