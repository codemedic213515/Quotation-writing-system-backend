using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CustomerMaster
{
    [Key]
    public int Id { get; set; } // Primary Key

    public string Group { get; set; } =string.Empty;// Customer Group
    public string Name { get; set; }  =string.Empty;// Customer Name
    public string SubName { get; set; }  =string.Empty;// Customer Subname
    public string Address { get; set; }  =string.Empty;// Address
    public string Phone { get; set; } =string.Empty; // Phone Number
    public int? CloseingDat { get; set; } // Closing Date
    public string Rank { get; set; }  =string.Empty;// Customer Rank
    public string Fax { get; set; }  =string.Empty;// Fax Number
    public string Email { get; set; }  =string.Empty;// Email Address
    public string Hp { get; set; } =string.Empty; // Homepage
    public string Creater { get; set; }  =string.Empty;// Creator Name
    public string Description { get; set; }  =string.Empty;// Description
    public bool? Delete { get; set; } // Deletion Status
}
}
