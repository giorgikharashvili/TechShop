using Microsoft.AspNetCore.Mvc;
using TechShop.Application.Services.Interfaces;
using TechShop.Domain.DTOs.Categories;
using TechShop.Application.Services;

namespace TechShop.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoryService;

        public CategoriesController(CategoriesService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Returns all categories.
        /// </summary>
        /// <returns>List of all categories.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriesDto>>> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Returns a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>Category with specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriesDto>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="dto">The category to create.</param>
        /// <returns>The newly created category.</returns>
        [HttpPost]
        public async Task<ActionResult<CategoriesDto>> Create([FromBody] CreateCategoriesDto dto)
        {
            var created = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">ID of the category to update.</param>
        /// <param name="dto">The updated category details.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoriesDto dto)
        {
            var exists = await _categoryService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _categoryService.UpdateAsync(id, dto);

            return NoContent();
        }

        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">ID of the category to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _categoryService.GetByIdAsync(id);
            if (exists == null) return NotFound();
            await _categoryService.DeleteAsync(id);
            

            return NoContent();
        }
    }
}
