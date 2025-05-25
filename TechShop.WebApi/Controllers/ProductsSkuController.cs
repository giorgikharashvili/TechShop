using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.ProductsSkus;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsSkuController : ControllerBase
    {
        private readonly ProductsSkusService _productsSkusService;

        public ProductsSkuController(ProductsSkusService productsSkusService)
        {
            _productsSkusService = productsSkusService;
        }

        /// <summary>
        /// Returns all product SKUs.
        /// </summary>
        /// <returns>List of all product SKUs.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsSkusDto>>> GetAll()
        {
            var result = await _productsSkusService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a product SKU by its ID.
        /// </summary>
        /// <param name="id">The ID of the product SKU to retrieve.</param>
        /// <returns>Product SKU with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsSkusDto>> GetById(int id)
        {
            var sku = await _productsSkusService.GetByIdAsync(id);
            if (sku == null) return NotFound();
            return Ok(sku);
        }

        /// <summary>
        /// Creates a new product SKU.
        /// </summary>
        /// <param name="dto">The product SKU to create.</param>
        /// <returns>The newly created product SKU.</returns>
        [HttpPost]
        public async Task<ActionResult<ProductsSkusDto>> Create([FromBody] CreateProductsSkusDto dto)
        {
            var created = await _productsSkusService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing product SKU.
        /// </summary>
        /// <param name="id">ID of the product SKU to update.</param>
        /// <param name="dto">The updated product SKU details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductsSkusDto dto)
        {
            var exists = await _productsSkusService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _productsSkusService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Deletes a product SKU by ID.
        /// </summary>
        /// <param name="id">ID of the product SKU to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _productsSkusService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _productsSkusService.DeleteAsync(id);

            return NoContent();
        }
    }
}
