using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.Payments;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsService _paymentsService;

        public PaymentsController(PaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }

        /// <summary>
        /// Returns all payments.
        /// </summary>
        /// <returns>List of all payments.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentsDto>>> GetAll()
        {
            var result = await _paymentsService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a payment by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        /// <returns>Payment with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentsDto>> GetById(int id)
        {
            var payment = await _paymentsService.GetByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        /// <summary>
        /// Creates a new payment.
        /// </summary>
        /// <param name="dto">The payment to create.</param>
        /// <returns>The newly created payment.</returns>
        [HttpPost]
        public async Task<ActionResult<PaymentsDto>> Create([FromBody] CreatePaymentDto dto)
        {
            var created = await _paymentsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing payment.
        /// </summary>
        /// <param name="id">ID of the payment status to update.</param>
        /// <param name="dto">The updated payment status.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentStatusDto dto)
        {
            var exists = await _paymentsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            var success = await _paymentsService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        /// <summary>
        /// Deletes a payment by ID.
        /// </summary>
        /// <param name="id">ID of the payment to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _paymentsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _paymentsService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
