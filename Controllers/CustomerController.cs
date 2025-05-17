using Microsoft.AspNetCore.Mvc;
using TechShop.Models;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static List<Customer> Customers = new List<Customer>
        {
            new Customer { Id = 1, Email = "johndoe@gmail.com", FullName="John Doe"},
            new Customer { Id = 2, Email = "giorgikharashvili@gmail.com", FullName="Giorgi Kharashvili"}
        };

        /// <summary>
        ///  Gets all customers
        /// </summary>
        /// <returns>List of customers</returns>
        // GET: api/customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAllCustomers()
        {
            return Ok(Customers);
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
            var customer = Customers.FirstOrDefault(c => c.Id == id);
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
            // Since no database to auto-generate ids
            customer.Id = Customers.Count > 0 ? Customers.Max(c => c.Id) + 1 : 1;
            Customers.Add(customer);

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
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
            var existingCustomer = Customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null) return NotFound();

            existingCustomer.FullName = customer.FullName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PasswordHash = customer.PasswordHash;
            existingCustomer.Orders = customer.Orders;

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
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();

            Customers.Remove(customer);

            return NoContent();
        }
        
    }
}
