using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Application.Features.OrderDetails.CreateOrderDetails;
using TechShop.Application.Features.OrderDetails.DeleteOrderDetails;
using TechShop.Application.Features.OrderDetails.UpdateOrderDetails;
using TechShop.Application.Features.OrderDetails.GetAllOrderDetails;
using TechShop.Application.Features.OrderDetails.GetOrderDetailsById;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController(IMediator _mediator, ILogger<OrderDetailsController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all order details.
    /// </summary>
    /// <returns>List of all order details.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<OrderDetailsDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all order details");

        var result = await _mediator.Send(new GetAllOrderDetailsQuery());

        _logger.LogInformation("Returned {Count} order details", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns order details by ID.
    /// </summary>
    /// <param name="id">The ID of the order details to retrieve.</param>
    /// <returns>Order detail with the specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<OrderDetailsDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching order details with ID: {Id}", id);

        var result = await _mediator.Send(new GetOrderDetailsByIdQuery(id));

        if (result == null)
        {
            _logger.LogWarning("Order details with ID: {Id} was not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates new order details.
    /// </summary>
    /// <param name="command">The order details to create.</param>
    /// <returns>The newly created order details.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<OrderDetailsDto>> Create([FromBody] CreateOrderDetailsCommand command)
    {
        _logger.LogInformation("Creating new order details");

        var result = await _mediator.Send(command);

        _logger.LogInformation("Created order");
        return Ok(result);
    }

    /// <summary>
    /// Updates existing order details.
    /// </summary>
    /// <param name="id">ID of the order details to update.</param>
    /// <param name="command">The updated order details data.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDetailsCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Order detail update failed: ID mismatch");
            return BadRequest("ID mismatch");
        }

        var updated = await _mediator.Send(command);

        if (!updated)
        {
            _logger.LogWarning("Update failed: Order details with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Order details with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes order details by ID.
    /// </summary>
    /// <param name="id">ID of the order details to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting order details with ID: {Id}", id);

        var deleted = await _mediator.Send(new DeleteOrderDetailsCommand(id));

        if (!deleted)
        {
            _logger.LogWarning("Delete failed: Order details with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Order details with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
