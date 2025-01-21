using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class PrefectureMaster
   {
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// Prefecture Name
    public bool? Delete { get; set; } // Deletion Status
}
}
