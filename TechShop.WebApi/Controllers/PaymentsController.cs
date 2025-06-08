using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.Application.Features.Payments.GetAllPayments;
using TechShop.Application.Features.Payments.CreatePayments;
using TechShop.Application.Features.Payments.DeletePayments;
using TechShop.Application.Features.Payments.GetPaymentsById;
using TechShop.Application.Features.Payments.UpdatePayments;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(IMediator _mediator, ILogger<PaymentsController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all payments.
    /// </summary>
    /// <returns>List of all payment records.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<ActionResult<IEnumerable<PaymentsDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all payment records");

        var result = await _mediator.Send(new GetAllPaymentsQuery());

        _logger.LogInformation("Returned {Count} payments", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a payment by ID.
    /// </summary>
    /// <param name="id">The ID of the payment to retrieve.</param>
    /// <returns>The payment with the specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<ActionResult<PaymentsDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching payment with ID: {Id}", id);

        var result = await _mediator.Send(new GetPaymentsByIdQuery(id));

        if (result == null)
        {
            _logger.LogWarning("Payment with ID: {Id} was not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new payment.
    /// </summary>
    /// <param name="command">The payment data to create.</param>
    /// <returns>The newly created payment.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<ActionResult<PaymentsDto>> Create([FromBody] CreatePaymentsCommand command)
    {
        _logger.LogInformation("Creating a new payment");

        var created = await _mediator.Send(command);

        _logger.LogInformation("Created payment with ID: {Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing payment.
    /// </summary>
    /// <param name="id">ID of the payment to update.</param>
    /// <param name="command">The updated payment data.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentsCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Payment update failed: ID mismatch");
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(command);

        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: Payment with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Payment with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a payment by ID.
    /// </summary>
    /// <param name="id">ID of the payment to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting payment with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeletePaymentsCommand(id));

        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: Payment with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Payment with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
