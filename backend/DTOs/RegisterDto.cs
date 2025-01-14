namespace QuotationWritingSystem.DTOs
{
    public class RegisterDto
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pwd { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
