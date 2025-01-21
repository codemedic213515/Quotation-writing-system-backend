using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
     
    public class ConstructionMaster
{
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; }=string.Empty; // Construction Name
    public bool? Delete { get; set; } // Deletion Status
    public double? SiteMiscell { get; set; } // Site Miscellaneous Costs
}
}
