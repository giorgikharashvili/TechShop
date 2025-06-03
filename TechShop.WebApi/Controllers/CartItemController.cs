using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Application.Features.Cart.GetAllCart;
using TechShop.Application.Features.Cart.UpdateCart;
using TechShop.Application.Features.CartItem.CreateCartItem;
using TechShop.Application.Features.CartItem.DeleteCartItem;
using TechShop.Application.Features.CartItem.GetCartItemById;
using TechShop.Domain.DTOs.CartItem;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all cart items.
        /// </summary>
        /// <returns>List of all cart items.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCartQuery());

            return Ok(result);
        }

        /// <summary>
        /// Returns a cart item by its ID.
        /// </summary>
        /// <param name="id">The ID of the cart item to retrieve.</param>
        /// <returns>Cart item with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<CartItemDto>> GetById(int id)
        {
            var cartItem = await _mediator.Send(new GetCartByIdQuery(id));
            if (cartItem == null) return NotFound();

            return Ok(cartItem);
        }

        /// <summary>
        /// Creates a new cart item.
        /// </summary>
        /// <param name="command">The cart item to create.</param>
        /// <returns>The newly created cart item.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<CartItemDto>> Create([FromBody] CreateCartItemCommand command)
        {
            var createdId = await _mediator.Send(command);
            var createdCartItem = await _mediator.Send(new GetCartByIdQuery(createdId.Id));

            return CreatedAtAction(nameof(GetById), new { id = createdId }, createdCartItem);
        }

        /// <summary>
        /// Updates an existing cart item.
        /// </summary>
        /// <param name="id">ID of the cart item to update.</param>
        /// <param name="command">The updated cart item details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartCommand command)
        {
            if (id != command.id) return BadRequest();

            var isSuccess = await _mediator.Send(command);
            if (!isSuccess) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a cart item by ID.
        /// </summary>
        /// <param name="id">ID of the cart item to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _mediator.Send(new DeleteCartItemCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}