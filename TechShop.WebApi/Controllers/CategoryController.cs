using Microsoft.AspNetCore.Mvc;
using TechShop.TechShop.Domain.Entites;
using TechShop.Application.Services;

namespace TechShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns>List of all categories.</returns>
        // GET: api/category
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return Ok(_categoryService.GetAllCategories());
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
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();
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
           var createCategory = _categoryService.PostCategory(category);

            return CreatedAtAction(nameof(GetById), new { id = createCategory.Id }, createCategory);
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
            var existingCategory = _categoryService.GetById(id);
            if (existingCategory == null) return NotFound();
            _categoryService.UpdateCategory(id, category);

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
            var category = _categoryService.GetById(id);
            if (category == null) return NotFound();
            _categoryService.DeleteCategory(id);

            return NoContent();
        }
    }
}
