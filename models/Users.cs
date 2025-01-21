using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class Users
   {
    [Key]
    public int Id { get; set; } // Primary Key

    public string Name { get; set; } =string.Empty;// User Name
    public string Email { get; set; } =string.Empty; // Email Address
    public string Password { get; set; }  =string.Empty;// User Password
    public string Code { get; set; }  =string.Empty;// User Code
    public string Role { get; set; }  =string.Empty;// User Role
    public bool? Delete { get; set; } // Deletion Status
}
}


