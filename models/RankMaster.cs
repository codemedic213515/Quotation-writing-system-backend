using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class RankMaster
    {
         [Required]
        public int Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public double LaborCostA { get; set; }
        public double LaborCostB { get; set; }
        public double SiteMiscell { get; set; }
        public double OtherExpens { get; set; }
    }
}
