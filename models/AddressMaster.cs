using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
public class AddressMaster
{
    [Key]
    public int Id { get; set; } // Primary Key

    public int? ZipCode { get; set; } // Postal Code
    public string Prefecture { get; set; } =string.Empty;// Prefecture Name
    public string City { get; set; }  =string.Empty;// City Name
    public string Street { get; set; }  =string.Empty;// Street Name
    public bool? Delete { get; set; } // Deletion Status
}
}
