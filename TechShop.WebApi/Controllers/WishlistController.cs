using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.Wishlist;
using TechShop.Application.Features.Wishlist.CreateWishlist;
using TechShop.Application.Features.Wishlist.DeleteWishlist;
using TechShop.Application.Features.Wishlist.GetAllWishlist;
using TechShop.Application.Features.Wishlist.GetWishlistById;
using TechShop.Application.Features.Wishlist.UpdateWishlist;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WishlistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all wishlist items.
        /// </summary>
        /// <returns>List of all wishlist items.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<WishlistDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllWishlistQuery());
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
            var item = await _mediator.Send(new GetWishlistByIdQuery(id));
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
        public async Task<ActionResult<WishlistDto>> Create([FromBody] CreateWishlistCommand dto)
        {
            var created = await _mediator.Send(dto);
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWishlistCommand dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var success = await _mediator.Send(dto);
            if (!success) return NotFound();

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
            var success = await _mediator.Send(new DeleteWishlistCommand(id));
            if (!success) return NotFound();

            return NoContent();
        }
    }
}