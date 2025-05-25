using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.OrderDetails;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OrderDetailsService _orderDetailsService;

        public OrderDetailsController(OrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        /// <summary>
        /// Returns all order details.
        /// </summary>
        /// <returns>List of all order details.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailsDto>>> GetAll()
        {
            var result = await _orderDetailsService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a specific order detail by ID.
        /// </summary>
        /// <param name="id">The ID of the order detail to retrieve.</param>
        /// <returns>Order detail with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetById(int id)
        {
            var detail = await _orderDetailsService.GetByIdAsync(id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        /// <summary>
        /// Creates a new order detail.
        /// </summary>
        /// <param name="dto">The order detail to create.</param>
        /// <returns>The newly created order detail.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderDetailsDto>> Create([FromBody] CreateOrderDetailsDto dto)
        {
            var created = await _orderDetailsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing order detail.
        /// </summary>
        /// <param name="id">ID of the order detail to update.</param>
        /// <param name="dto">The updated order detail data.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDetailsDto dto)
        {
            var exists = await _orderDetailsService.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _orderDetailsService.UpdateAsync(id, dto);
         

            return NoContent();
        }

        /// <summary>
        /// Deletes an order detail by ID.
        /// </summary>
        /// <param name="id">ID of the order detail to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _orderDetailsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _orderDetailsService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
