using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class YearMaster
 {
    [Key]
    public long Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// Year Name
    public int? StartYear { get; set; } // Start Year
}
}
