using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TechShop.Application.Features.Address.CreateAddresses;
using TechShop.Application.Features.Address.DeleteAddresses;
using TechShop.Application.Features.Address.GetAddressesById;
using TechShop.Application.Features.Address.GetAllAddresses;
using TechShop.Application.Features.Address.UpdateAddresses;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.Constants;
using TechShop.Domain.DTOs.Addresses;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController(IMediator _mediator, ILogger<AddressesController> _logger, IAddressesService _addressesService) : ControllerBase
    {
        /// <summary>
        /// Returns all addresses.
        /// </summary>
        /// <returns>List of all addresses.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<ActionResult<IEnumerable<AddressesDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all addresses");

            var result = await _mediator.Send(new GetAllAddressQuery());

            _logger.LogInformation("Returned {Count} addresses", result.Count());
            return Ok(result);
        }

        /// <summary>
        /// Returns an address by its ID.
        /// </summary>
        /// <param name="id">The ID of the address to retrieve.</param>
        /// <returns>Address with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<ActionResult<AddressesDto>> GetById(int id)
        {
            _logger.LogInformation("Fetching address with given Id: {Id}", id);
            var address = await _mediator.Send(new GetAddressesByIdQuery(id));

            if (address == null)
            {
                _logger.LogWarning("Address with given Id: {Id} was not found", id);
                return NotFound();
            }
            
            return Ok(address);
        }


        /// <summary>
        /// gets an addresses by country.
        /// </summary>
        /// <param name="country">country of the addresses to get.</param>
        /// <returns>addresses by its country.</returns>
        [HttpGet("country/{country}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<ActionResult<IEnumerable<AddressesDto>>> GetByCountry(string country)
        {
            _logger.LogInformation("Returning addresses with given Country: {country}", country);

            var result = await _addressesService.GetByCountryAsync(country);
            if (result == null) return NotFound();

            _logger.LogInformation("Returned addresses with given Country: {country} successfully", country);
            return Ok(result);
        }


        /// <summary>
        /// gets an address by city.
        /// </summary>
        /// <param name="city">city of the addresses to get.</param>
        /// <returns>addresses by its city.</returns>
        [HttpGet("city/{city}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<ActionResult<IEnumerable<AddressesDto>>> GetByCity(string city)
        {
            _logger.LogInformation("Returning addresses with given City: {city}", city);

            var result = await _addressesService.GetByCityAsync(city);
            if (result == null) return NotFound();

            _logger.LogInformation("Returned addresses with given City: {city} successfully", city);
            return Ok(result);
        }

        /// <summary>
        /// gets an address by postal Code.
        /// </summary>
        /// <param name="postalCode">postal Code of the addresses to get.</param>
        /// <returns>addresses by its postal Code.</returns>
        [HttpGet("postalCode/{postalCode}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<ActionResult<IEnumerable<AddressesDto>>> GetByPostalCode(string postalCode)
        {
            _logger.LogInformation("Returning addresses with given PostalCode: {postalCode}", postalCode);

            var result = await _addressesService.GetByPostalCodeAsync(postalCode);
            if (result == null) return NotFound();

            _logger.LogInformation("Returned addresses with given PostalCode: {postalCode} successfully", postalCode);
            return Ok(result);
        }


        /// <summary>
        /// Creates a new address.
        /// </summary>
        /// <param name="dto">The address to create.</param>
        /// <returns>The newly created address.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Customer}")]
        public async Task<ActionResult<AddressesDto>> Create([FromBody] CreateAddressCommand command)
        {
            _logger.LogInformation("Creating address for {UserId}", command.Dto.UserId);

            var createdAddress = await _mediator.Send(command);

            _logger.LogInformation("Created address with Id: {Id}", createdAddress.Id);
            return CreatedAtAction(nameof(GetById), new { id = createdAddress.Id }, createdAddress);
        }

        /// <summary>
        /// Updates an existing address.
        /// </summary>
        /// <param name="id">ID of the address to update.</param>
        /// <param name="dto">The updated address details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressesCommand command)
        {
            if (id != command.id)
            {
                _logger.LogWarning("Address update failed: ID mismatch");
                return NotFound(0);
            }

            var isSuccess = await _mediator.Send(command);

            if (!isSuccess)
            {
                _logger.LogWarning("Update failed: address with given Id: {Id} was not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updated address with Id {Id} successfully", id);
            return NoContent();
        }

        /// <summary>
        /// Deletes an address by ID.
        /// </summary>
        /// <param name="id">ID of the address to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting address with given Id: {Id}", id);

            var isSuccess = await _mediator.Send(new DeleteAddressCommand(id));

            if (!isSuccess)
            {
                _logger.LogInformation("Delete failed: address with given Id: {Id} was not found", id);
                return NotFound();
            }

            _logger.LogInformation("Deleted address with given Id: {Id} successfully", id);
            return NoContent();
        }
    }
}
