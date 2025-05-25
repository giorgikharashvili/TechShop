namespace TechShop.TechShop.Domain.Entites
{
    public class Discount
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
