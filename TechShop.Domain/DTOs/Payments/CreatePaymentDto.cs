using System.ComponentModel.DataAnnotations;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Domain.DTOs.Payments
{
    public class CreatePaymentDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string StripePaymentId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(10)]
        public string Currency { get; set; }

        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public string StripeSessionId { get; set; }
    }
}
