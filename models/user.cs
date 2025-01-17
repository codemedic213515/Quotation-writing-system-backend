using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Code {get; set;} = string.Empty;
        [Required]
        public string Name {get; set;} = string.Empty;
        [Required]
        public string Role { get; set; } = "user";
    }
}

