using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;


namespace TechShop.TechShop.Domain.Entities
{
    [DbSchema("auth")]
    public class Addresses
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public string AddressLine1  { get; set; }
        public string AddressLine2  { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }


    }
}
