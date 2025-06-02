using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkus;
using TechShop.Application.Features.ProductsSkus.CreateProductsSkus;
using TechShop.Application.Features.ProductsSkus.DeleteProductsSkus;
using TechShop.Application.Features.ProductsSkus.GetAllProductsSkus;
using TechShop.Application.Features.ProductsSkus.GetProductsSkusById;
using TechShop.Application.Features.ProductsSkus.UpdateProductsSkus;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsSkuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsSkuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all product SKUs.
        /// </summary>
        /// <returns>List of all product SKUs.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<ProductsSkusDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsSkusQuery());
            return Ok(result);
        }

        /// <summary>
        /// Returns a product SKU by its ID.
        /// </summary>
        /// <param name="id">The ID of the product SKU to retrieve.</param>
        /// <returns>Product SKU with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<ProductsSkusDto>> GetById(int id)
        {
            var sku = await _mediator.Send(new GetProductsSkusByIdQuery(id));
            if (sku == null) return NotFound();
            return Ok(sku);
        }

        /// <summary>
        /// Creates a new product SKU.
        /// </summary>
        /// <param name="command">The product SKU to create.</param>
        /// <returns>The newly created product SKU.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<ProductsSkusDto>> Create([FromBody] CreateProductsSkusCommand command)
        {
            var created = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing product SKU.
        /// </summary>
        /// <param name="id">ID of the product SKU to update.</param>
        /// <param name="command">The updated product SKU details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsSkusCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            var success = await _mediator.Send(command);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes a product SKU by ID.
        /// </summary>
        /// <param name="id">ID of the product SKU to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteProductsSkusCommand(id));
            if (!success) return NotFound();
            return NoContent();
        }
    }
}