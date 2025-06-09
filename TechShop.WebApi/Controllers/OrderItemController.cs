using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Application.Features.OrderItem.CreateOrderItem;
using TechShop.Application.Features.OrderItem.GetAllOrderItem;
using TechShop.Application.Features.OrderItem.GetOrderItemById;
using TechShop.Application.Features.OrderItem.UpdateOrderItem;
using TechShop.Application.Features.OrderItem.DeleteOrderItem;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController(IMediator _mediator, ILogger<OrderItemsController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all order items.
    /// </summary>
    /// <returns>List of all order items.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all order items");

        var result = await _mediator.Send(new GetAllOrderItemQuery());

        _logger.LogInformation("Returned {Count} order items", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns an order item by ID.
    /// </summary>
    /// <param name="id">The ID of the order item to retrieve.</param>
    /// <returns>The order item with the specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<OrderItemDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching order item with ID: {Id}", id);

        var result = await _mediator.Send(new GetOrderItemByIdQuery(id));

        if (result == null)
        {
            _logger.LogWarning("Order item with ID: {Id} was not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new order item.
    /// </summary>
    /// <param name="command">The order item data to create.</param>
    /// <returns>The newly created order item.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<OrderItemDto>> Create([FromBody] CreateOrderItemCommand command)
    {
        _logger.LogInformation("Creating a new order item");

        var result = await _mediator.Send(command);

        _logger.LogInformation("Created order item");
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing order item.
    /// </summary>
    /// <param name="id">ID of the order item to update.</param>
    /// <param name="command">The updated order item data.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderItemCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Order item update failed: ID mismatch");
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(command);

        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: Order item with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Order item with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes an order item by ID.
    /// </summary>
    /// <param name="id">ID of the order item to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting order item with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteOrderItemCommand(id));

        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: Order item with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Order item with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
