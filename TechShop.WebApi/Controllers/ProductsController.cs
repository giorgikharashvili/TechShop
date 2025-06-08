using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MediatR;
using TechShop.Domain.DTOs.Products;
using TechShop.Application.Features.Products.GetAllProducts;
using TechShop.Application.Features.Products.CreateProducts;
using TechShop.Application.Features.Products.DeleteProducts;
using TechShop.Application.Features.Products.GetProductsById;
using TechShop.Application.Features.Products.UpdateProducts;
using TechShop.Application.Features.Products.CreateFullProduct;
using TechShop.Domain.Constants;
using TechShop.Application.Features.Products.GetByCategoryId;

namespace TechShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator _mediator, ILogger<ProductsController> _logger) : ControllerBase
{
    /// <summary>
    /// Returns all products.
    /// </summary>
    [HttpGet]
    [EnableRateLimiting("RequestsLimiter")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductsDto>>> GetAll()
    {
        _logger.LogInformation("Fetching all products");

        var result = await _mediator.Send(new GetAllProductsQuery());

        _logger.LogInformation("Returned {Count} products", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Returns a product by ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    [HttpGet("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<ProductsDto>> GetById(int id)
    {
        _logger.LogInformation("Fetching product with ID: {Id}", id);

        var result = await _mediator.Send(new GetProductsByIdQuery(id));
        if (result == null)
        {
            _logger.LogWarning("Product with ID: {Id} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Returns products by category ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    [HttpGet("category/{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<IEnumerable<ProductsDto>>> GetByCategoryId(int id)
    {
        _logger.LogInformation("Fetching products with category ID: {Id}", id);

        var result = await _mediator.Send(new GetByCategoryIdQuery(id));
        if (result == null)
        {
            _logger.LogWarning("Products with category ID: {Id} not found", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="command">The product data to create.</param>
    [HttpPost]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult<ProductsDto>> Create([FromBody] CreateProductsCommand command)
    {
        _logger.LogInformation("Creating product: {Name}", command.Dto.Name);

        var created = await _mediator.Send(command);

        _logger.LogInformation("Created product with ID: {Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">ID of the product to update.</param>
    /// <param name="command">The updated product data.</param>
    [HttpPut("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsCommand command)
    {
        if (id != command.id)
        {
            _logger.LogWarning("Product update failed: ID mismatch");
            return BadRequest("ID mismatch");
        }

        var isSuccess = await _mediator.Send(command);
        if (!isSuccess)
        {
            _logger.LogWarning("Update failed: Product with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Product with ID {Id} updated successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="id">ID of the product to delete.</param>
    [HttpDelete("{id}")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Deleting product with ID: {Id}", id);

        var isSuccess = await _mediator.Send(new DeleteProductsCommand(id));
        if (!isSuccess)
        {
            _logger.LogWarning("Delete failed: Product with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Product with ID {Id} deleted successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Creates a full product with its SKUs and attributes in one request.
    /// </summary>
    /// <param name="command">The full product structure to create.</param>
    [HttpPost("full")]
    [EnableRateLimiting("RequestsLimiter")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
    public async Task<ActionResult> CreateFullProduct([FromBody] CreateFullProductCommand command)
    {
        _logger.LogInformation("Creating full product structure");

        var productId = await _mediator.Send(command);

        _logger.LogInformation("Created full product with ID: {Id}", productId);
        return CreatedAtAction(nameof(GetById), new { id = productId }, null);
    }
}
