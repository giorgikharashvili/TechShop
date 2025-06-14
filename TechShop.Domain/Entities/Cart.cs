﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("cart")]
    public class Cart
    {
        public int Id { get; init; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
