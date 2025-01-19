using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CategoryMaterialMaster4
    {
         [Required]
        public int Id { get; set; }
        public int Category1 { get; set; }
        public int Category2 { get; set; }
        public int Category3 { get; set; }
        public int Category4 { get; set; }
        public string Name { get; set; }= string.Empty;
        public int SelectFlag { get; set; }
        public int DetailFlag { get; set; }
        public bool? Delete { get; set; }
        public float Material { get; set; }
    }
}
