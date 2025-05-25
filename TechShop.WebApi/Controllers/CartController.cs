using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Returns all carts.
        /// </summary>
        /// <returns>List of all cart entries.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAll()
        {
            var result = await _cartService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a cart by its ID.
        /// </summary>
        /// <param name="id">The ID of the cart to retrieve.</param>
        /// <returns>Cart with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetById(int id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        /// <summary>
        /// Creates a new cart.
        /// </summary>
        /// <param name="dto">The cart to create.</param>
        /// <returns>The newly created cart.</returns>
        [HttpPost]
        public async Task<ActionResult<CartDto>> Create([FromBody] CreateCartDto dto)
        {
            var created = await _cartService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing cart.
        /// </summary>
        /// <param name="id">ID of the cart to update.</param>
        /// <param name="dto">The updated cart details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartDto dto)
        {
            var exists = await _cartService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _cartService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Deletes a cart by ID.
        /// </summary>
        /// <param name="id">ID of the cart to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _cartService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _cartService.DeleteAsync(id);
           
            return NoContent();
        }
    }
}
