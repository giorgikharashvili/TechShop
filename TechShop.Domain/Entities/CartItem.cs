using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("cart")]
    public class CartItem
    {
        public int Id { get; init; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int ProductSkuId { get; set; }
        public int Quantity { get; set; }
     
    }
}
