using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MediatR;
using TechShop.Domain.DTOs.Payments;
using TechShop.Application.Features.Payments.GetAllPayments;
using TechShop.Application.Features.Payments.CreatePayments;
using TechShop.Application.Features.Payments.DeletePayments;
using TechShop.Application.Features.Payments.GetPaymentsById;
using TechShop.Application.Features.Payments.UpdatePayments;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<IEnumerable<PaymentsDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllPaymentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<PaymentsDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetPaymentsByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<ActionResult<PaymentsDto>> Create([FromBody] CreatePaymentsCommand command)
        {
            var created = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentsCommand command)
        {
            if (id != command.id) return BadRequest("ID mismatch");
            var success = await _mediator.Send(command);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("RequestsLimiter")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeletePaymentsCommand(id));
            if (!success) return NotFound();
            return NoContent();
        }
    }
}