using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("cart")]
    public class Wishlist
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; init; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

    }
}
