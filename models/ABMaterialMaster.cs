using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class ABMaterialMaster
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }= string.Empty;
        [Required]
        public int ABCode { get; set; }
        [Required]
        public float Rate { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public string CategoryName { get; set; }= string.Empty;
        public float OtherRate { get; set; }
        public bool? Delete { get; set; }
    }
}
