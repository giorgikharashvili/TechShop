using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Application.Features.Cart.CreateCart;
using TechShop.Application.Features.Cart.DeleteCart;
using TechShop.Application.Features.Cart.UpdateCart;
using TechShop.Application.Features.Cart.GetAllCart;
using TechShop.Application.Features.Cart.GetCartById;
using TechShop.Domain.DTOs.Cart;
using TechShop.Domain.Constants;
using TechShop.Application.Features.Products.CreateFullProduct;
using TechShop.Application.Features.Cart.CreateFullCart;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(IMediator _mediator, ILogger<CartController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all carts.
    /// </summary>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<CartDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all cart entries");

        var result = await _mediator.Send(new GetAllCartQuery());

        _logger.LogInformation("Returned {Count} cart entries", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a cart by its ID.
    /// </summary>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<CartDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching cart with given Id: {Id}", id);

        var cart = await _mediator.Send(new GetCartByIdQuery(id));

        if (cart == null)
        {
            _logger.LogWarning("Cart with given Id: {Id} was not found", id);
            return NotFound();
        }

        return Ok(cart);
    }

    /// <summary>
    /// Creates a new cart.
    /// </summary>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
    public async Task<ActionResult<CartDto>> Create([FromBody] CreateCartCommand command)
    {
        _logger.LogInformation("Creating cart");

        var createdId = await _mediator.Send(command);
        var createdCart = await _mediator.Send(new GetCartByIdQuery(createdId.Id));

        _logger.LogInformation("Created cart with ID: {CartId}", createdCart.Id);
        return CreatedAtAction(nameof(GetById), new { id = createdCart.Id }, createdCart);
    }

    /// <summary>
    /// Updates an existing cart.
    /// </summary>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCartCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Cart update failed: ID mismatch (Route: {RouteId}, Payload: {PayloadId})", id, command.id);
            return BadRequest();
        }

        var isSuccess = await _mediator.Send(command);

        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: cart with given Id: {Id} was not found", id);
            return NotFound();
        }

        _logger.LogInformation("Updated cart with Id {Id} successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a cart by ID.
    /// </summary>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting cart with given Id: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteCartCommand(id));

        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: cart with given Id: {Id} was not found", id);
            return NotFound();
        }

        _logger.LogInformation("Deleted cart with given Id: {Id} successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Creates a full cart with its items one request.
    /// </summary>
    /// <param name="command">The full cart structure to create.</param>
    [HttpPost("full")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult> CreateFullCart([FromBody] CreateFullCartCommand command)
    {
        _logger.LogInformation("Creating full cart structure");

        var productId = await _mediator.Send(command);

        _logger.LogInformation("Created full product with ID: {Id}", productId);
        return CreatedAtAction(nameof(GetById), new { id = productId }, null);
    }
}
