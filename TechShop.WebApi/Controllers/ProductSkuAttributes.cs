using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.ProductsSkuAttributes;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductSkuAttributesController : ControllerBase
    {
        private readonly ProductSkuAttributesService _skuAttributesService;

        public ProductSkuAttributesController(ProductSkuAttributesService skuAttributesService)
        {
            _skuAttributesService = skuAttributesService;
        }

        /// <summary>
        /// Returns all product SKU attributes.
        /// </summary>
        /// <returns>List of all product SKU attributes.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<ProductSkuAttributesDto>>> GetAll()
        {
            var result = await _skuAttributesService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a product SKU attribute by its ID.
        /// </summary>
        /// <param name="id">The ID of the product SKU attribute to retrieve.</param>
        /// <returns>Product SKU attribute with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<ProductSkuAttributesDto>> GetById(int id)
        {
            var attribute = await _skuAttributesService.GetByIdAsync(id);
            if (attribute == null) return NotFound();
            return Ok(attribute);
        }

        /// <summary>
        /// Creates a new product SKU attribute.
        /// </summary>
        /// <param name="dto">The product SKU attribute to create.</param>
        /// <returns>The newly created product SKU attribute.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<ProductSkuAttributesDto>> Create([FromBody] CreateProductSkuAttributesDto dto)
        {
            var created = await _skuAttributesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing product SKU attribute.
        /// </summary>
        /// <param name="id">ID of the product SKU attribute to update.</param>
        /// <param name="dto">The updated product SKU attribute details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsSkuAttributesDto dto)
        {
            var exists = await _skuAttributesService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _skuAttributesService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Deletes a product SKU attribute by ID.
        /// </summary>
        /// <param name="id">ID of the product SKU attribute to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _skuAttributesService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _skuAttributesService.DeleteAsync(id);

            return NoContent();
        }
    }
}
