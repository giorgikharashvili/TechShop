using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.ProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.CreateProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.DeleteProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.GetAllProductsSkuAttributes;
using TechShop.Application.Features.ProductsSkuAttributes.GetProductsSkuAttributesById;
using TechShop.Application.Features.ProductsSkuAttributes.UpdateProductsSkuAttributes;
using Microsoft.AspNetCore.Authorization;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductSkuAttributesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductSkuAttributesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all product SKU attributes.
        /// </summary>
        /// <returns>List of all product SKU attributes.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Manager}")]
        public async Task<ActionResult<IEnumerable<ProductSkuAttributesDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsSkuAttributesQuery());

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
            var result = await _mediator.Send(new GetProductsSkuAttributesByIdQuery(id));
            if (result == null) return NotFound();

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
            var created = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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
            if (id != command.id) return BadRequest("ID mismatch");

            var isSuccess = await _mediator.Send(command);
            if (!isSuccess) return NotFound();

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
            var isSuccess = await _mediator.Send(new DeleteProductsSkuAttributesCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}