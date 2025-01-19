using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class QuotationMain
    {
         [Required]
        public int Id { get; set; }
        public string Code { get; set; }= string.Empty;
        public string Creater { get; set; }= string.Empty;
        public string Name { get; set; }= string.Empty;
        public string Address { get; set; }= string.Empty;
        public string Export { get; set; }= string.Empty;
        public string Import { get; set; }= string.Empty;
        public string Purpose { get; set; }= string.Empty;
        public double? Square { get; set; }
        public string Standard { get; set; }= string.Empty;
    }
}
