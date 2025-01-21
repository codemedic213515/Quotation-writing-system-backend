using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class UnitMaster
   {
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// Unit Name
    public bool? Delete { get; set; } // Deletion Status
}
}
