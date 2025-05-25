using Microsoft.AspNetCore.Mvc;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
       
        /// <summary>
        /// Gets all discounts
        /// </summary>
        /// <returns>List of discounts</returns>
        // GET: api/discount
        [HttpGet]
        public ActionResult<IEnumerable<Discount>> GetAllDiscounts()
        {
            return Ok(Discounts);
        }

        /// <summary>
        /// Gets discount by specific Id
        /// </summary>
        /// <param name="id">Id of the discount to get</param>
        /// <returns>Discount with specific Id</returns>
        // GET: api/discount/{id}
        [HttpGet("{id}")]
        public ActionResult<Discount> GetById(int id)
        {
            var discount = Discounts.FirstOrDefault(d => d.Id == id);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        /// <summary>
        /// Creates new discount
        /// </summary>
        /// <param name="discount">The discount to create</param>
        /// <returns></returns>
        // POST: api/discount
        [HttpPost] 
        public ActionResult<Discount> PostDiscount(Discount discount)
        {
            // Since no database to auto-generate ids
            discount.Id = Discounts.Count > 0 ? Discounts.Max(c => c.Id) + 1 : 1;
            Discounts.Add(discount);

            return CreatedAtAction(nameof(GetById), new { id = discount.Id }, discount);
        }


        /// <summary>
        /// Updates existing discount
        /// </summary>
        /// <param name="id">Id of the discount to update</param>
        /// <param name="discount">The updated discount</param>
        /// <returns>No content if successful</returns>
        // PUT: api/discount/{id}
        [HttpPut("{id}")]
        public ActionResult<Discount> UpdateDiscount(int id,[FromBody] Discount discount)
        {
            var existingDiscount = Discounts.FirstOrDefault(d => d.Id == id);
            if (discount == null) return NotFound();

            existingDiscount.ProductId = discount.ProductId;
            existingDiscount.DiscountAmount = discount.DiscountAmount;
            existingDiscount.StartDate = discount.StartDate;
            existingDiscount.EndDate = discount.EndDate;

            return NoContent();
        }

        /// <summary>
        /// Delets discount
        /// </summary>
        /// <param name="id">Id of the discount to delete</param>
        /// <returns>No content if succcessful</returns>
        // DELETE: api/discount/{id}
        [HttpDelete("{id}")]
        public ActionResult<Discount> DeleteDiscount(int id)
        {
            var discount = Discounts.FirstOrDefault(d => d.Id == id);
            if (discount == null) return NotFound();

            Discounts.Remove(discount);

            return NoContent();
        }


       
    }
}
