using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class CalculationData
    {
[Key]        public int Id { get; set; } // Primary Key
        public int PipeAccessoryRate { get; set; }
        public int PipeSupportRate { get; set; }
        public int CableRackAccessoryRate { get; set; }
        public int CableRackSupportRate { get; set; }
        public int RacewayAccessoryRate { get; set; }
        public int RacewaySupportRate { get; set; }
        public int CableAdditionalRate { get; set; }
        public int LightingAdditionalRate { get; set; }
        public int PanelAdditionalRate { get; set; }
        public bool PerformAuxiliaryWorks { get; set; }
        public int AuxiliaryWorkRate { get; set; }
        public int OverheadRate { get; set; }
        public int CostRate { get; set; }
    }
}
