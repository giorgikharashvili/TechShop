using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Users
{
    public class UpdateUserDto
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}
