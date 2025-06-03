using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Application.Features.OrderDetails.CreateOrderDetails;
using TechShop.Domain.DTOs.OrderDetails;
using TechShop.Application.Features.OrderDetails.UpdateOrderDetails;
using TechShop.Application.Features.OrderDetails.GetAllOrderDetails;
using TechShop.Application.Features.OrderDetails.GetOrderDetailsById;
using TechShop.Application.Features.OrderDetails.DeleteOrderDetails;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<OrderDetailsDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrderDetailsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<OrderDetailsDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetOrderDetailsByIdQuery(id));
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<OrderDetailsDto>> Create([FromBody] CreateOrderDetailsCommand command)
        {
            var created = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDetailsCommand command)
        {
            if (id != command.id) return BadRequest("ID mismatch");

            var updated = await _mediator.Send(command);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteOrderDetailsCommand(id));
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}