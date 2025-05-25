using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services;
using TechShop.TechShop.Domain.Entites;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
       
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns>List of all products</returns>
        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
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
            var product = _productService.GetById(id);
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
            var createProduct = _productService.PostProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = createProduct.Id }, createProduct);
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The id of the product</param>
        /// <param name="product">The updated product</param>
        /// <returns>No Content if successful</returns>
        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id,[FromBody] Product product)
        {
            var existingProduct = _productService.GetById(id);
            if (existingProduct == null) return NotFound();
            _productService.UpdateProduct(id, product);

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
            var product = _productService.GetById(id);
            if (product == null) return NotFound();

            _productService.DeleteProduct(id);

            return NoContent();
        }
    }
}
