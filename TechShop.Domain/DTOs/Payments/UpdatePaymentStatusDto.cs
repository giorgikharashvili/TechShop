using System.ComponentModel.DataAnnotations;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Domain.DTOs.Payments
{
    public class UpdatePaymentStatusDto
    {
        // For stripe webhook hanlder to update the payment status
        [Required]
        public OrderStatus Status { get; set; }
    }
}
