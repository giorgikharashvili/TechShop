using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.CartItem;

public class CreateFullCartItemDto
{
    [Required]
    public int ProductId { get; set; }

    [Required]
    public string ProductSkuId { get; set; }

    [Required]
    public int Quantity { get; set; }
}
