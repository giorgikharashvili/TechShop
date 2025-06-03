using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.Users;
using TechShop.Application.Features.Users.CreateUsers;
using TechShop.Application.Features.Users.DeleteUsers;
using TechShop.Application.Features.Users.GetAllUsers;
using TechShop.Application.Features.Users.GetUsersById;
using TechShop.Application.Features.Users.UpdateUsers;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns>List of all users.</returns>
        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            return Ok(result);
        }

        /// <summary>
        /// Returns a user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>User with specified ID.</returns>
        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _mediator.Send(new GetUsersByIdQuery(id));
            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto">The user to create.</param>
        /// <returns>The newly created user.</returns>
        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUsersCommand dto)
        {
            var created = await _mediator.Send(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">ID of the user to update.</param>
        /// <param name="dto">The updated user details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUsersCommand dto)
        {
            if (id != dto.id) return BadRequest("ID mismatch");

            var isSuccess = await _mediator.Send(dto);
            if (!isSuccess) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">ID of the user to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _mediator.Send(new DeleteUsersCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}