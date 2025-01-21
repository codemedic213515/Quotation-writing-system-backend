using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CategoryMaterialMaster1
{
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// Material Category Name
    public bool? Delete { get; set; } // Deletion Status
}
}
