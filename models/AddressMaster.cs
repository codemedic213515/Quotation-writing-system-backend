using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
 public class AddressMaster
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public bool Delete { get; set; } // Change from string to bool
     [Required]
    public string Prefecture { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}
}
