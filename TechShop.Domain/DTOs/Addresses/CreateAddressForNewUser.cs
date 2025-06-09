using System.ComponentModel.DataAnnotations;

namespace TechShop.Domain.DTOs.Addresses;

public class CreateAddressForNewUser
{
    [Required]
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public int PostalCode { get; set; }
}
