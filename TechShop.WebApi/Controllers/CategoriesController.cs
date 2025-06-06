using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Application.Features.Categories.CreateCategories;
using TechShop.Application.Features.Categories.DeleteCategories;
using TechShop.Application.Features.Categories.GetAllCategories;
using TechShop.Application.Features.Categories.GetCategoriesById;
using TechShop.Application.Features.Categories.UpdateCategories;
using TechShop.Domain.DTOs.Categories;
using Microsoft.AspNetCore.Authorization;
using TechShop.Domain.Constants;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Customer}, {UserRoles.Admin}")]
        public async Task<ActionResult<IEnumerable<CategoriesDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<ActionResult<CategoriesDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCategoriesByIdQuery(id));
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<ActionResult<CategoriesDto>> Create([FromBody] CreateCategoriesCommand command)
        {
            var created = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = created.Id}, created);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriesCommand command)
        {
            if (id != command.id) return BadRequest("ID mismatch");

            var isSuccess = await _mediator.Send(command);
            if (!isSuccess) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        [Authorize(Roles = $"{UserRoles.Admin}, {UserRoles.Manager}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _mediator.Send(new DeleteCategoriesCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}