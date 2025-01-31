using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class QuotationType
    {
        [Key]
        public int Id { get; set; } // Primary Key

        public string Number { get; set; } = string.Empty; // Quotation Type Number
        public string Category1 { get; set; } = string.Empty; // Category Level 1
        public string Category2 { get; set; } = string.Empty; // Category Level 2
        public string Category3 { get; set; } = string.Empty; // Category Level 3
        public string Category4 { get; set; } = string.Empty; // Category Level 4
        public double? RemovalRate { get; set; } // Removal Rate
        public bool? Calculate { get; set; }
        public bool? Delete { get; set; } = false; // Default value for Deletion Status
    }
}