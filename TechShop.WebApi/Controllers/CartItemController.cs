using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Application.Features.Cart.GetAllCart;
using TechShop.Application.Features.Cart.UpdateCart;
using TechShop.Application.Features.CartItem.CreateCartItem;
using TechShop.Application.Features.CartItem.DeleteCartItem;
using TechShop.Application.Features.CartItem.GetCartItemById;
using TechShop.Domain.DTOs.CartItem;
using Microsoft.AspNetCore.Authorization;
using TechShop.Domain.Constants;
using TechShop.Application.Features.CartItem.UpdateCartItem;

namespace TechShop.WebApi.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class CartItemsController(IMediator _mediator, ILogger<CartItemsController> _logger) : ControllerBase
    {
        /// <summary>
        /// Returns all cart items.
        /// </summary>
        /// <returns>List of all cart items.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetAll()
        {
            _logger.LogInformation("Fetching All Cart Items");
            var result = await _mediator.Send(new GetAllCartQuery());

            _logger.LogInformation("Returned {Count} cart items", result.Count());
            return Ok(result);
        }

        /// <summary>
        /// Returns a cart item by its ID.
        /// </summary>
        /// <param name="id">The ID of the cart item to retrieve.</param>
        /// <returns>Cart item with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<ActionResult<CartItemDto>> GetById(int id)
        {
            _logger.LogInformation("Fetching cart item with given Id: {id}", id);
            var cartItem = await _mediator.Send(new GetCartByIdQuery(id));
            if (cartItem == null)
            {
            _logger.LogWarning("Cart item with given Id: {Id} was not found", id);
            return NotFound();
            }

             _logger.LogInformation("Returned Cart item with given Id: {Id}", id);
             return Ok(cartItem);
        }

        /// <summary>
        /// Creates a new cart item.
        /// </summary>
        /// <param name="command">The cart item to create.</param>
        /// <returns>The newly created cart item.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
        public async Task<ActionResult<CartItemDto>> Create([FromBody] CreateCartItemCommand command)
        {
            _logger.LogInformation("Creating Cart Item");
            var result = await _mediator.Send(command);

            _logger.LogInformation("Created cart item");
            return Ok(result);
        }

        /// <summary>
        /// Updates an existing cart item.
        /// </summary>
        /// <param name="id">ID of the cart item to update.</param>
        /// <param name="command">The updated cart item details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartItemCommand command)
        {   
           _logger.LogInformation("Cart Item Update failed, ID mismatch");
           if (id != command.id) return BadRequest();

           var isSuccess = await _mediator.Send(command);
           if (!isSuccess)
           {
                _logger.LogWarning("Update failed: Cart Item with given Id: {Id} was not found", id);
                return NotFound();
           }

           _logger.LogInformation("Updated Cart Item with Id {Id} successfully", id);
           return NoContent();
        }

        /// <summary>
        /// Deletes a cart item by ID.
        /// </summary>
        /// <param name="id">ID of the cart item to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
           _logger.LogInformation("Deleting Cart item with given Id: {Id}", id);

           var isSuccess = await _mediator.Send(new DeleteCartItemCommand(id));

           if (!isSuccess)
           {
               _logger.LogInformation("Delete failed: Cart Item with given Id: {Id} was not found", id);
               return NotFound();
           }

           return NoContent();
        }
    }
