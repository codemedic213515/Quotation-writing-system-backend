using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class QuotationType
    {
        [Required]
        public int Id { get; set; }
        public int Number { get; set; }
        public int Category1 { get; set; }
        public int Category2 { get; set; }
        public int Category3 { get; set; }
        public int Category4 { get; set; }
        public double RemovalRate { get; set; }
        public bool? Delete { get; set; }
    }
}
