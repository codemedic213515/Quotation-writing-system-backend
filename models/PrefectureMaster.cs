using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class PrefectureMaster
    {
         [Required]
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public bool? Delete { get; set; }
    }
}
