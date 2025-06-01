using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TechShop.Application.Features.Address.CreateAddresses;
using TechShop.Application.Features.Address.DeleteAddresses;
using TechShop.Application.Features.Address.GetAddressesById;
using TechShop.Application.Features.Address.GetAllAddresses;
using TechShop.Application.Features.Address.UpdateAddresses;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all addresses.
        /// </summary>
        /// <returns>List of all addresses.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<AddressesDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCartItemQuery());
            return Ok(result);
        }

        /// <summary>
        /// Returns an address by its ID.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>Address with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<AddressesDto>> GetById(int id)
        {
            var address = await _mediator.Send(new GetAddressesByIdQuery(id));
            if (address == null) return NotFound();
            return Ok(address);
        }

        /// <summary>
        /// Creates a new address.
        /// </summary>
        /// <param name="dto">The address to create.</param>
        /// <returns>The newly created address.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<AddressesDto>> Create([FromBody] CreateCartItemCommand command)
        {
            var createdId = await _mediator.Send(command);

            var createdAddress = await _mediator.Send(new GetAddressesByIdQuery(createdId));
            return CreatedAtAction(nameof(GetById), new { id = createdId }, createdAddress);
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">ID of the address to update.</param>
        /// <param name="dto">The updated address details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCartCommand command)
        {
            if (id != command.id) return BadRequest();
            var success = await _mediator.Send(command);
            if (!success) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Deletes an address by ID.
        /// </summary>
        /// <param name="id">ID of the address to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteCartItemCommand(id));
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
