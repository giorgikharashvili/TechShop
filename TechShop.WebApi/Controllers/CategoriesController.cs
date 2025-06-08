using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Application.Features.Categories.CreateCategories;
using TechShop.Application.Features.Categories.DeleteCategories;
using TechShop.Application.Features.Categories.GetAllCategories;
using TechShop.Application.Features.Categories.GetCategoriesById;
using TechShop.Application.Features.Categories.UpdateCategories;
using TechShop.Domain.DTOs.Categories;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(IMediator _mediator, ILogger<CategoriesController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all categories.
    /// </summary>
    /// <returns>List of all categories.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
    public async Task<ActionResult<IEnumerable<CategoriesDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all categories");

        var result = await _mediator.Send(new GetAllCategoriesQuery());

        _logger.LogInformation("Returned {Count} categories", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>Category with specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}")]
    public async Task<ActionResult<CategoriesDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching category with ID: {Id}", id);

        var result = await _mediator.Send(new GetCategoriesByIdQuery(id));

        if (result == null)
        {
            _logger.LogWarning("Category with ID: {Id} was not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="command">The category data to create.</param>
    /// <returns>The newly created category.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<ActionResult<CategoriesDto>> Create([FromBody] CreateCategoriesCommand command)
    {
        _logger.LogInformation("Creating new category");

        var created = await _mediator.Send(command);

        _logger.LogInformation("Created category with ID: {Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="id">ID of the category to update.</param>
    /// <param name="command">The updated category details.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriesCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Category update failed: ID mismatch (Route: {RouteId}, Payload: {PayloadId})", id, command.id);
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(command);
        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: Category with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Category with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    /// <param name="id">ID of the category to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting category with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteCategoriesCommand(id));

        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: Category with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Category with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
