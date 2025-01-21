using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
  public class ABMaterialMaster
{
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// Material Name
    public string ABCode { get; set; } =string.Empty;// AB Code
    public double? Rate { get; set; } // Rate of Material
    public double? Cost { get; set; } // Cost of Material
    public string CategoryName { get; set; }=string.Empty; // Category Name
    public double? OtherRate { get; set; } // Other Rate
    public bool? Delete { get; set; } // Deletion Status
}
}
