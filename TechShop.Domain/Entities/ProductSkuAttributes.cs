using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("catalog")]
    public class ProductSkuAttributes
    {
        public int Id { get; init; }
        public string Type { get; set; }
        public string Value { get; set; }

    }
}
