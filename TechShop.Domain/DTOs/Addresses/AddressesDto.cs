namespace TechShop.Domain.DTOs.Addresses
{
    public class AddressesDto
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public int UserId { get; set; }
    }
}
