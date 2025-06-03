using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Users
{
    public class UpdateUserDto
    {
        [MaxLength(50)]
        public string Username { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
}
