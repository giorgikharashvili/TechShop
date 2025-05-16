using Microsoft.AspNetCore.Mvc;
using TechShop.Models;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Alienware Rtx 4090 I9 32GB Ram", Description = "Gaming PC for latest games", StockQuantity = 10, Price = 5000, CategoryId = 1},
            new Product { Id = 2, Name = "HyperX Alloy Origins 60 Mechaincal Keyboard", Description = "Hyperx Mechanical Switches Petite 60% form factor", StockQuantity=30, Price = 99, CategoryId = 3 }
        };



        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>List of all products</returns>
        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(Products);
        }


        /// <summary>
        /// Returns a product by its id
        /// </summary>
        /// <param name="id">The id of the product to get</param>
        /// <returns>The product with the specific id</returns>
        // GET: api/product/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product and adds it to the list
        /// </summary>
        /// <param name="product">The product to create</param>
        /// <returns>Newly created product</returns>
        // POST: api/product
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            // Since no database to auto-generate ids
            product.Id = Products.Count > 0 ? Products.Max(c => c.Id) + 1 : 1;
            Products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product); 
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <param name="product">The updated product</param>
        /// <returns>No Content if successful</returns>
        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null) return NotFound();
            
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.CategoryId = product.CategoryId;

            return NoContent();
        }

        /// <summary>
        /// Deletes Product
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <returns>No Content if successful</returns>
        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public ActionResult<Product> DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            Products.Remove(product);

            return NoContent();
        }
    }
}
