﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("catalog")]
    public class ProductsSkus
    {
        public int Id { get; init; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public string StockQuantity { get; set; }
    }
}
