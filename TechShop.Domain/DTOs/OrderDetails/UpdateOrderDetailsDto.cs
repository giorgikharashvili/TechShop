using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Domain.DTOs.OrderDetails
{
    public class UpdateOrderDetailsDto
    {
        [Required]
        public int TotalPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
