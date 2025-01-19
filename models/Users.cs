using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class Users
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public string Code { get; set; }= string.Empty;
        public string Role {get; set;}=string.Empty;
        public bool? Delete { get; set; }
    }
}


