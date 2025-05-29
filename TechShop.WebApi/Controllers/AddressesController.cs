using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly AddressesService _addressService;

        public AddressesController(AddressesService addressService)
        {
            _addressService = addressService;
        }

        /// <summary>
        /// Returns all addresses.
        /// </summary>
        /// <returns>List of all addresses.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<AddressesDto>>> GetAll()
        {
            var result = await _addressService.GetAllAsync();
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
            var address = await _addressService.GetByIdAsync(id);
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
        public async Task<ActionResult<AddressesDto>> Create([FromBody] CreateAddressesDto dto)
        {
            var created = await _addressService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">ID of the address to update.</param>
        /// <param name="dto">The updated address details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressesDto dto)
        {
            var exists = await _addressService.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _addressService.UpdateAsync(id, dto);

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
            var exists = await _addressService.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _addressService.DeleteAsync(id);
            

            return NoContent();
        }
    }
}
