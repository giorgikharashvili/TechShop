using System.ComponentModel.DataAnnotations;


namespace TechShop.Domain.DTOs.OrderDetails
{
    public class CreateOrderDetailsDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalPrice { get; set; }
    }
}
