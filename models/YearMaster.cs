using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class YearMaster
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public int StartYear { get; set; }
    }
}
