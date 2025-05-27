using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Products
{
    public class UpdateProductDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
