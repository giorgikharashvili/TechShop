using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("orders")]
    public class OrderDetails
    {
        public int Id { get; init; }
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
