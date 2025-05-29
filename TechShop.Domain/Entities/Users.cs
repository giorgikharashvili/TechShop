using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechShop.Domain.Attributes;

namespace TechShop.Domain.Entities
{
    [DbSchema("auth")]
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
