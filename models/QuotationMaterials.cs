using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class QuotationMaterials
    {
        [Required]
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int Category1 { get; set; }
        public int Category2 { get; set; }
        public int Category3 { get; set; }
        public int Category4 { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }= string.Empty;
        public double Price { get; set; }
        public double Step { get; set; }
        public double Divide { get; set; }
        public string CategoryName { get; set; }= string.Empty;
    }
}
