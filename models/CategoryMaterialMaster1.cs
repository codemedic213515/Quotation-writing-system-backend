using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CategoryMaterialMaster1
    {
         [Required]
        public int Id { get; set; }
         [Required]
        public string Name { get; set; }= string.Empty;
        public int SelectFlag { get; set; }
        public int DetailFlag { get; set; }
        public bool? Delete { get; set; }
        public float Material { get; set; }
    }
}
