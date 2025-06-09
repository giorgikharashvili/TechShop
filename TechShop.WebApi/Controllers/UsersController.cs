using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Domain.DTOs.Users;
using TechShop.Application.Features.Users.CreateUsers;
using TechShop.Application.Features.Users.DeleteUsers;
using TechShop.Application.Features.Users.GetAllUsers;
using TechShop.Application.Features.Users.GetUsersById;
using TechShop.Application.Features.Users.UpdateUsers;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IMediator _mediator, ILogger<UsersController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all users.
    /// </summary>
    /// <returns>List of all users.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all users");

        var result = await _mediator.Send(new GetAllUsersQuery());

        _logger.LogInformation("Returned {Count} users", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>User with specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching user with ID: {Id}", id);

        var user = await _mediator.Send(new GetUsersByIdQuery(id));

        if (user == null)
        {
            _logger.LogWarning("User with ID {Id} not found", id);
            return NotFound();
        }

        return Ok(user);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="dto">The user to create.</param>
    /// <returns>The newly created user.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<UserDto>> Create([FromBody] CreateUsersCommand dto)
    {
        _logger.LogInformation("Creating new user");

        var result = await _mediator.Send(dto);

        _logger.LogInformation("Created user");
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="id">ID of the user to update.</param>
    /// <param name="dto">The updated user details.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUsersCommand dto)
    {
        if (id != dto.id)
        {
            _logger.LogWarning("User update failed");
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(dto);
        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: User with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("User with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a user by ID.
    /// </summary>
    /// <param name="id">ID of the user to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting user with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteUsersCommand(id));
        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: User with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("User with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
