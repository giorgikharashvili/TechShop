using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.Domain.DTOs.OrderItem;


namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderItemService _orderItemsService;

        public OrderItemsController(OrderItemService orderItemsService)
        {
            _orderItemsService = orderItemsService;
        }

        /// <summary>
        /// Returns all order items.
        /// </summary>
        /// <returns>List of all order items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll()
        {
            var result = await _orderItemsService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns an order item by its ID.
        /// </summary>
        /// <param name="id">The ID of the order item to retrieve.</param>
        /// <returns>Order item with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDto>> GetById(int id)
        {
            var item = await _orderItemsService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Creates a new order item.
        /// </summary>
        /// <param name="dto">The order item to create.</param>
        /// <returns>The newly created order item.</returns>
        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> Create([FromBody] CreateOrderItemDto dto)
        {
            var created = await _orderItemsService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing order item.
        /// </summary>
        /// <param name="id">ID of the order item to update.</param>
        /// <param name="dto">The updated order item details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderItemDto dto)
        {
            var exists = await _orderItemsService.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _orderItemsService.UpdateAsync(id, dto);
            
            return NoContent();
        }

        /// <summary>
        /// Deletes an order item by ID.
        /// </summary>
        /// <param name="id">ID of the order item to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _orderItemsService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _orderItemsService.DeleteAsync(id);
            
            return NoContent();
        }
    }
}
