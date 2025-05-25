using TechShop.TechShop.Domain.Enums;

namespace TechShop.TechShop.Domain.Entites
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<Product> Products { get; set; }

    }
}
