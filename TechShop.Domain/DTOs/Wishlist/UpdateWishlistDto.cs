using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Wishlist
{
    public class UpdateWishlistDto
    {
        [Required]
        public int ProductId { get; set; }
    }
}
