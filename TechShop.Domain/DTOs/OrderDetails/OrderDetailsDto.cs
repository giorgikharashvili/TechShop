using System;

namespace TechShop.Domain.DTOs.OrderDetails
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
    }
}
