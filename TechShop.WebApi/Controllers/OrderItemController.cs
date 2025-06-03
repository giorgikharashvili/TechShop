using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.OrderItem;
using TechShop.Application.Features.OrderItem.CreateOrderItem;
using TechShop.Application.Features.OrderItem.GetAllOrderItem;
using TechShop.Application.Features.OrderItem.GetOrderItemById;
using TechShop.Application.Features.OrderItem.UpdateOrderItem;
using TechShop.Application.Features.OrderItem.DeleteOrderItem;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllOrderItemQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<OrderItemDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetOrderItemByIdQuery(id));
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<OrderItemDto>> Create([FromBody] CreateOrderItemCommand command)
        {
            var created = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderItemCommand command)
        {
            if (id != command.id) return BadRequest("ID mismatch");

            var isSuccess = await _mediator.Send(command);
            if (!isSuccess) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _mediator.Send(new DeleteOrderItemCommand(id));
            if (!isSuccess) return NotFound();

            return NoContent();
        }
    }
}