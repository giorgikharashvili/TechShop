using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.CartItem;


namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private readonly CartItemService _cartItemsService;

        public CartItemsController(CartItemService cartItemsService)
        {
            _cartItemsService = cartItemsService;
        }

        /// <summary>
        /// Returns all cart items.
        /// </summary>
        /// <returns>List of all cart items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAll()
        {
            var result = await _cartItemsService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a cart item by its ID.
        /// </summary>
        /// <param name="id">The ID of the cart item to retrieve.</param>
        /// <returns>Cart item with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemDto>> GetById(int id)
        {
            var cartItem = await _cartItemsService.GetByIdAsync(id);
            if (cartItem == null) return NotFound();
            return Ok(cartItem);
        }

        /// <summary>
        /// Creates a new cart item.
        /// </summary>
        /// <param name="dto">The cart item to create.</param>
        /// <returns>The newly created cart item.</returns>
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> Create([FromBody] CreateCartItemDto dto)
        {
            var created = await _cartItemsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing cart item.
        /// </summary>
        /// <param name="id">ID of the cart item to update.</param>
        /// <param name="dto">The updated cart item details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartItemDto dto)
        {
            var exists = await _cartItemsService.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _cartItemsService.UpdateAsync(id, dto);
            

            return NoContent();
        }

        /// <summary>
        /// Deletes a cart item by ID.
        /// </summary>
        /// <param name="id">ID of the cart item to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _cartItemsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _cartItemsService.DeleteAsync(id);

            return NoContent();
        }
    }
}
