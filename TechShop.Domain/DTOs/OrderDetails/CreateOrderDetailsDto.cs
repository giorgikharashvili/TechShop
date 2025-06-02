using System.ComponentModel.DataAnnotations;


namespace TechShop.Domain.DTOs.OrderDetails
{
    public class CreateOrderDetailsDto
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TotalPrice { get; set; }
    }
}
