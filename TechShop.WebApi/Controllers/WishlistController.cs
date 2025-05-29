using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.Wishlist;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly WishlistService _wishlistService;

        public WishlistController(WishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        /// <summary>
        /// Returns all wishlist items.
        /// </summary>
        /// <returns>List of all wishlist items.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetAll()
        {
            var result = await _wishlistService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a wishlist item by its ID.
        /// </summary>
        /// <param name="id">The ID of the wishlist item to retrieve.</param>
        /// <returns>Wishlist item with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<WishlistDto>> GetById(int id)
        {
            var item = await _wishlistService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Creates a new wishlist item.
        /// </summary>
        /// <param name="dto">The wishlist item to create.</param>
        /// <returns>The newly created wishlist item.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<WishlistDto>> Create([FromBody] CreateWishlistDto dto)
        {
            var created = await _wishlistService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing wishlist item.
        /// </summary>
        /// <param name="id">ID of the wishlist item to update.</param>
        /// <param name="dto">The updated wishlist item details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWishlistDto dto)
        {
            var exists = await _wishlistService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _wishlistService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Deletes a wishlist item by ID.
        /// </summary>
        /// <param name="id">ID of the wishlist item to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _wishlistService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _wishlistService.DeleteAsync(id);

            return NoContent();
        }
    }
}
