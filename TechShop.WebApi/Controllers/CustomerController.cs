using Microsoft.AspNetCore.Mvc;
using TechShop.TechShop.Domain.Entites;
using TechShop.Application.Services;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        ///  Gets all customers
        /// </summary>
        /// <returns>List of customers</returns>
        // GET: api/customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(_customerService.GetAllCustomers());
        }

       
        /// <summary>
        /// Gets customer by specific Id
        /// </summary>
        /// <param name="id">Id of the customer to get</param>
        /// <returns>Customer with specific Id</returns>
        // GET: api/customer/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        /// <summary>
        /// Creates a new Customer
        /// </summary>
        /// <param name="customer">The customer to create</param>
        /// <returns>Newly created customer</returns>
        // POST: api/customer
        [HttpPost]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            var createCustomer = _customerService.PostCustomer(customer);
          
            return CreatedAtAction(nameof(GetById), new { id = createCustomer.Id }, createCustomer);
        }


        /// <summary>
        /// Updates existing customer
        /// </summary>
        /// <param name="id">Id of the customer to update</param>
        /// <param name="customer">The updated customer</param>
        /// <returns>No content if successful</returns>
        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public ActionResult<Customer> UpdateCustomer(int id,[FromBody] Customer customer)
        {
            var existingCustomer = _customerService.GetById(id);
            if (existingCustomer == null) return NotFound();
            _customerService.UpdateCustomer(id, customer);

            return NoContent();
        }


        /// <summary>
        /// Deletes customer
        /// </summary>
        /// <param name="id">Id of the customer to delete</param>
        /// <returns>No content if successful</returns>
        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null) return NotFound();
            _customerService.DeleteCustomer(id);

            return NoContent();
        }
        
    }
}
