using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Categories
{
    public class UpdateCategoriesDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
