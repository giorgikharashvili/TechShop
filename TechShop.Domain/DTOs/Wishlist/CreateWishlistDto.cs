using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Wishlist
{
    public class CreateWishlistDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
