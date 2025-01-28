using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class QuotationMain
{
    [Key]
    public int Id { get; set; } // Primary Key

    public string Code { get; set; } =string.Empty;// Quotation Code
    public string Creater { get; set; }  =string.Empty;// Creator
    public string Name { get; set; } =string.Empty; // Quotation Name
    public string Address { get; set; } =string.Empty; // Quotation Address
    public string Export { get; set; } =string.Empty; // Export Details
    public string Import { get; set; } =string.Empty; // Import Details
    public string Purpose { get; set; }  =string.Empty;// Purpose of Quotation
    public string Square { get; set; }  =string.Empty;// Square Measurement
    public string Standard { get; set; }  =string.Empty;// Standard Description
    public bool? Delete { get; set; } // Deletion Status
    public DateTime CreatedAt { get; set; } = DateTime.Now; // Creation Date
}
}
