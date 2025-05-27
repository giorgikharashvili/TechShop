using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Domain.DTOs.Payments
{
    public class PaymentsDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string StripePaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public OrderStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
