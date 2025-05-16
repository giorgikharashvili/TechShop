using Microsoft.AspNetCore.Mvc;
using TechShop.Models;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> Categories = new List<Category>
        {
            new Category {Id = 1, Name = "Personal Computers (PC)", Description = "Gaming and Office Personal Computers"},
            new Category {Id = 2, Name = "Mouses", Description = "Gaming and office Mouses"},
            new Category {Id = 3, Name = "Mechanical Keyboards", Description = "Gaming and office Keyboards"},
        };
        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>List of all categories.</returns>
        // GET: api/category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return Ok(Categories);
        }

        /// <summary>
        /// Returns Category by its id
        /// </summary>
        /// <param name="id">the Id of the category to get</param>
        /// <returns>Category with specific Id.</returns>
        // GET: api/category/{id}
        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) NotFound();

            return Ok(category);
        }

        /// <summary>
        /// Creates new Category and adds it to the list
        /// </summary>
        /// <param name="category">the category to create</param>
        /// <returns>Newly created product.</returns>
        // POST: api/category
        [HttpPost]
        public ActionResult<Category> PostCategory(Category category)
        {
            // Since no database to auto-generate ids
            category.Id = Categories.Count > 0 ? Categories.Max(c => c.Id) + 1 : 1;
            Categories.Add(category);

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        /// <summary>
        /// Updates existing category with Id
        /// </summary>
        /// <param name="id">Id of the product to update</param>
        /// <param name="category">The updated product</param>
        /// <returns>No content if successful</returns>
        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public ActionResult<Category> UpdateCategory(int id,[FromBody] Category category)
        {
            var existingCategory = Categories.FirstOrDefault(c => c.Id == id);
            if (existingCategory == null) return NotFound();

            existingCategory.Id = category.Id;
            existingCategory.Name = category.Name;

            return NoContent();
        }

        /// <summary>
        /// Delets Category
        /// </summary>
        /// <param name="id">Id of the product to delete</param>
        /// <returns>No content if successful</returns>
        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            Categories.Remove(category);

            return NoContent();
        }
    }
}
