using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Domain.DTOs.OrderItem
{
    public class UpdateOrderItemDto
    {
    
        [Required]
        public int Quantity { get; set; }

    }
}
