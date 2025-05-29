using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;
using TechShop.TechShop.Domain.Enums;

namespace TechShop.Domain.Entities
{
    [DbSchema("orders")]
    public class Payments
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string StripePaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
