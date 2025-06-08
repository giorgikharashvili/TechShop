using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Application.Features.ProductsSkus.CreateProductsSkus;
using TechShop.Application.Features.ProductsSkus.DeleteProductsSkus;
using TechShop.Application.Features.ProductsSkus.GetAllProductsSkus;
using TechShop.Application.Features.ProductsSkus.GetProductsSkusById;
using TechShop.Application.Features.ProductsSkus.UpdateProductsSkus;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsSkuController(IMediator _mediator, ILogger<ProductsSkuController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all product SKUs.
    /// </summary>
    /// <returns>List of all product SKUs.</returns>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<ProductsSkusDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all product SKUs");

        var result = await _mediator.Send(new GetAllProductsSkusQuery());

        _logger.LogInformation("Returned {Count} product SKUs", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a product SKU by ID.
    /// </summary>
    /// <param name="id">The ID of the product SKU to retrieve.</param>
    /// <returns>Product SKU with specified ID.</returns>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<ProductsSkusDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching product SKU with ID: {Id}", id);

        var sku = await _mediator.Send(new GetProductsSkusByIdQuery(id));

        if (sku == null)
        {
            _logger.LogWarning("Product SKU with ID {Id} not found", id);
            return NotFound();
        }

        return Ok(sku);
    }

    /// <summary>
    /// Creates a new product SKU.
    /// </summary>
    /// <param name="command">The product SKU to create.</param>
    /// <returns>The newly created product SKU.</returns>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<ProductsSkusDto>> Create([FromBody] CreateProductsSkusCommand command)
    {
        _logger.LogInformation("Creating new product SKU");

        var created = await _mediator.Send(command);

        _logger.LogInformation("Created product SKU");
        return Ok(command.Dto);
    }

    /// <summary>
    /// Updates an existing product SKU.
    /// </summary>
    /// <param name="id">ID of the product SKU to update.</param>
    /// <param name="command">The updated product SKU details.</param>
    /// <returns>No content if successful.</returns>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsSkusCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Product SKU update failed: ID mismatch");
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(command);
        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: Product SKU with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Product SKU with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a product SKU by ID.
    /// </summary>
    /// <param name="id">ID of the product SKU to delete.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting product SKU with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteProductsSkusCommand(id));

        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: Product SKU with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Product SKU with ID {Id} deleted successfully", id);
        return NoContent();
    }
}
