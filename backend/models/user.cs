// File: Models/User.cs
namespace QuotationWritingSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Init { get; set; } = string.Empty; // 2-character unique code
        public string Role { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
    }
}
