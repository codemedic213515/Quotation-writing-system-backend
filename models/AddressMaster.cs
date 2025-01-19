using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class AddressMaster
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ZipCode { get; set; }= string.Empty;
        [Required]
        public string Prefecture { get; set; }= string.Empty;
        [Required]
        public string City { get; set; }= string.Empty;
        [Required]
        public string Street { get; set; }= string.Empty;
        public bool? Delete { get; set; }
    }
}
