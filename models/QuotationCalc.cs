using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotationWritingSystem.Models
{
    public class QuotationCalc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Set Id to be auto-increment
        public int Id { get; set; }

        public decimal TubeNetRate { get; set; }
        public decimal TubeReplenishmentRate { get; set; }
        public decimal CableNetRate { get; set; }
        public decimal CableReplenishmentRate { get; set; }

        public string Rank { get; set; } = string.Empty; // Use string instead of nvarchar(max)
        public int LaborCostA { get; set; }
        public int LaborCostB { get; set; }

        public decimal SiteMiscellRate { get; set; }
        public decimal MiscellRate { get; set; }
        public int Minority { get; set; }

        public decimal LaborBasisRate { get; set; }
        public bool ABMethod { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}