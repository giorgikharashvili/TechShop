using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.DeleteProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.GetAllProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.GetProductsSkuAttributesById;
using TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductSkuAttributesController(IMediator _mediator, ILogger<ProductSkuAttributesController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all product SKU attributes.
    /// </summary>
    /// <returns>List of all product SKU attributes.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<ProductSkuAttributesDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all product SKU attributes");

        var result = await _mediator.Send(new GetAllProductsSkuAttributesQuery());

        _logger.LogInformation("Returned {Count} product SKU attributes", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a product SKU attribute by its ID.
    /// </summary>
    /// <param name="id">The ID of the product SKU attribute to retrieve.</param>
    /// <returns>Product SKU attribute with specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<ProductSkuAttributesDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching product SKU attribute with ID: {Id}", id);

        var result = await _mediator.Send(new GetProductsSkuAttributesByIdQuery(id));
        if (result == null)
        {
            _logger.LogWarning("Product SKU attribute with ID: {Id} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new product SKU attribute.
    /// </summary>
    /// <param name="command">The product SKU attribute to create.</param>
    /// <returns>The newly created product SKU attribute.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<ProductSkuAttributesDto>> Create([FromBody] CreateProductsSkuAttributesCommand command)
    {
        _logger.LogInformation("Creating new product SKU attribute");

        var created = await _mediator.Send(command);

        _logger.LogInformation("Created product SKU");
        return Ok(command.Dto);
    }

    /// <summary>
    /// Updates an existing product SKU attribute.
    /// </summary>
    /// <param name="id">ID of the product SKU attribute to update.</param>
    /// <param name="command">The updated product SKU attribute details.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsSkuAttributesCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Update failed: ID mismatch");
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(command);
        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: Product SKU attribute with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Updated product SKU attribute with ID: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a product SKU attribute by ID.
    /// </summary>
    /// <param name="id">ID of the product SKU attribute to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting product SKU attribute with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteProductsSkuAttributesCommand(id));
        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: Product SKU attribute with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Deleted product SKU attribute with ID: {Id} successfully", id);
        return NoContent();
    }
}
