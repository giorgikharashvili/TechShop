using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("catalog")]
    public class Categories
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
