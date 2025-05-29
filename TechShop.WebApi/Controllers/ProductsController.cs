using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.Products;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsService _productsService;

        public ProductsController(ProductsService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Returns all products.
        /// </summary>
        /// <returns>List of all products.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<ProductsDto>>> GetAll()
        {
            var result = await _productsService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>Product with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<ProductsDto>> GetById(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="dto">The product to create.</param>
        /// <returns>The newly created product.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<ProductsDto>> Create([FromBody] CreateProductDto dto)
        {
            var created = await _productsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">ID of the product to update.</param>
        /// <param name="dto">The updated product details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            var exists = await _productsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _productsService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">ID of the product to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _productsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _productsService.DeleteAsync(id);
            

            return NoContent();
        }
    }
}
