using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class RankMaster
{
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// Rank Name
    public double? LaborCostA { get; set; } // Labor Cost A
    public double? LaborCostB { get; set; } // Labor Cost B
    public double? SiteMiscell { get; set; } // Site Miscellaneous Cost
    public double? OtherExpens { get; set; } // Other Expenses
    public bool? Delete { get; set; } // Deletion Status
}
}
