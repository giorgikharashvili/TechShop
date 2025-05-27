namespace TechShop.Domain.DTOs.CartItem
{
    public class CartWithItemsDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public List<CartItemDto> Items { get; set; }
    }
}
