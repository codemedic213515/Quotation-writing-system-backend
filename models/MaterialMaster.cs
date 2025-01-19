using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class MaterialMaster
    {
         [Required]
        public int Id { get; set; }
        public int Category1 { get; set; }
        public int Category2 { get; set; }
        public int Category3 { get; set; }
        public string CategoryName { get; set; }= string.Empty;
        public string Name { get; set; }= string.Empty;
        public float ExternalCost { get; set; }
        public float InternalCost { get; set; }
        public float BuildingCost { get; set; }
        public float AccumulatedRate { get; set; }
        public float CompositeCost { get; set; }
        public float StepRateB { get; set; }
        public float RemovalRateB { get; set; }
        public float Other { get; set; }
        public string Unit { get; set; }= string.Empty;
        public int MaterialCategoryNumber { get; set; }
        public string MaterialCategoryName { get; set; }= string.Empty;
        public int ABCMaterial { get; set; }
        public string UpdateDate { get; set; }= string.Empty;
        public string Updater { get; set; }= string.Empty;
        public int CeilingOpen { get; set; }
    }
}
