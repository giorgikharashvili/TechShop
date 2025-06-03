using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Application.Features.Cart.CreateCart;
using TechShop.Application.Features.Cart.DeleteCart;
using TechShop.Application.Features.Cart.UpdateCart;
using TechShop.Application.Features.Cart.GetAllCart;
using TechShop.Application.Features.Cart.GetCartById;
using TechShop.Domain.DTOs.Cart;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all carts.
        /// </summary>
        /// <returns>List of all cart entries.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCartQuery());

            return Ok(result);
        }

        /// <summary>
        /// Returns a cart by its ID.
        /// </summary>
        /// <param name="id">The ID of the cart to retrieve.</param>
        /// <returns>Cart with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<CartDto>> GetById(int id)
        {
            var cart = await _mediator.Send(new GetCartByIdQuery(id));
            if (cart == null) return NotFound();

            return Ok(cart);
        }

        /// <summary>
        /// Creates a new cart.
        /// </summary>
        /// <param name="command">The cart to create.</param>
        /// <returns>The newly created cart.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<CartDto>> Create([FromBody] CreateCartCommand command)
        {
            var createdId = await _mediator.Send(command);
            var createdCart = await _mediator.Send(new GetCartByIdQuery(createdId.Id));

            return CreatedAtAction(nameof(GetById), new { id = createdCart.Id }, createdCart);
        }

        /// <summary>
        /// Updates an existing cart.
        /// </summary>
        /// <param name="id">ID of the cart to update.</param>
        /// <param name="command">The updated cart details.</param>
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
        /// Deletes a cart by ID.
        /// </summary>
        /// <param name="id">ID of the cart to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _mediator.Send(new DeleteCartCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}