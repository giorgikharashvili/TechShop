using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("catalog")]
    public class Products
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; init; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }


    }
}
