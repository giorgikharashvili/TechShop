using Microsoft.AspNetCore.Mvc;
using TechShop.TechShop.Domain.Entites;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static List<Order> Orders = new List<Order>
        {
            new Order
            {
                Id = 1 ,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Delivered,
                TotalAmount = 5500
            }
            
        };

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns>List of orders</returns>
        // GET: api/order
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            return Ok(Orders);
        }

        /// <summary>
        /// Gets order by specific Id
        /// </summary>
        /// <param name="id">Id of the order to get</param>
        /// <returns>Order with specific Id</returns>
        // GET: api/order/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            return Ok(order);
        }

        /// <summary>
        /// Creates new order
        /// </summary>
        /// <param name="order">The order to create</param>
        /// <returns>Newly created order</returns>
        // POST: api/order
        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            // Since no database to auto-generate ids
            order.Id = Orders.Count > 0 ? Orders.Max(c => c.Id) + 1 : 1;
            Orders.Add(order);

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Updates existing order
        /// </summary>
        /// <param name="id">Id of the order to update</param>
        /// <param name="order">the order to update</param>
        /// <returns>No content if successful</returns>
        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public ActionResult<Order> UpdateOrder(int id,[FromBody] Order order)
        {
            var existingOrder = Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null) return NotFound();

            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Products = order.Products;

            return NoContent();
        }

        /// <summary>
        /// Delets order
        /// </summary>
        /// <param name="id">Id of the order to delete</param>
        /// <returns>No content if successful</returns>
        // DELETE: api/order/{id} 
        [HttpDelete("{id}")]
        public ActionResult<Order> DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();

            Orders.Remove(order);

            return NoContent();
        }
    }
}
