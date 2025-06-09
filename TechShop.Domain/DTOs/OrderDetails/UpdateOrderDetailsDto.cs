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
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}
