using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CategoryMaterialMaster2
{
    [Key]
    public int Id { get; set; } // Primary Key (Auto-Increment)

    public int? Category1 { get; set; } // Parent Category ID
    public int? Category2 { get; set; } // Subcategory ID
    public string Name { get; set; } =string.Empty;// Material Category Name
    public bool? Delete { get; set; } // Deletion Status
}

}
