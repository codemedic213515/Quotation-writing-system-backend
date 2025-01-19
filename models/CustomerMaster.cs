using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CustomerMaster
    {
         [Required]
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Contact { get; set; }= string.Empty;
        public string Address { get; set; }= string.Empty;
        public bool? Delete { get; set; }
    }
}
