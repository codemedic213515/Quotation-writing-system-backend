using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class QuotationMaterial
{
    [Key]
    public int Id { get; set; } // Primary Key

    public int? TypeId { get; set; } // Type ID
    public string Category1 { get; set; }=string.Empty; // Category Level 1
    public string Category2 { get; set; } =string.Empty; // Category Level 2
    public string Category3 { get; set; } =string.Empty; // Category Level 3
    public string Category4 { get; set; } =string.Empty; // Category Level 4
    public string Quantity { get; set; } =string.Empty; // Quantity
    public string Unit { get; set; }=string.Empty;  // Unit of Measurement
    public string Price { get; set; } =string.Empty; // Price
    public string StepRate { get; set; } =string.Empty; // Step Rate
    public string Divide { get; set; } =string.Empty; // Division Type
    public string Category { get; set; } =string.Empty; // Category Name
    public bool? Delete { get; set; }
}
}
