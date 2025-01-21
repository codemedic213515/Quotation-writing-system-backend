using System.ComponentModel.DataAnnotations;

namespace QuotationWritingSystem.Models
{
    public class MaterialMaster
 {
    [Key]
    public int Id { get; set; } // Primary Key (Auto-Increment)

    public int? Category1 { get; set; } // Parent Category ID
    public int? Category2 { get; set; } // Subcategory ID
    public int? Category3 { get; set; } // Tertiary Category ID
    public string CategoryNam { get; set; }=string.Empty; // Category Name
    public string Name { get; set; } =string.Empty;// Material Name
    public double? ExternalCos { get; set; } // External Cost
    public double? InternalCos { get; set; } // Internal Cost
    public double? BuildingCos { get; set; } // Building Cost
    public double? Accumulated { get; set; } // Accumulated Costs
    public double? CompositeCo { get; set; } // Composite Costs
    public double? ElectricalI { get; set; } // Electrical Installation Cost
    public double? SupplyRate { get; set; } // Supply Rate
    public double? AccessoryRa { get; set; } // Accessory Rate
    public double? Miscellaneo { get; set; } // Miscellaneous Costs
    public int? LaborCostA { get; set; } // Labor Cost A
    public double? StepRateA { get; set; } // Step Rate A
    public double? RemovalRate { get; set; } // Removal Rate
    public int? LaborCostB { get; set; } // Labor Cost B
    public double? StepRateB { get; set; } // Step Rate B
    public double? RemovalRate1 { get; set; } // Removal Rate 1
    public double? Other { get; set; } // Other Costs
    public string Unit { get; set; } =string.Empty;// Unit of Measurement
    public string MaterialCat { get; set; } =string.Empty;// Material Category
    public string MaterialCat1 { get; set; } =string.Empty;// Secondary Material Category
    public int? ABC_Materia { get; set; } // ABC Material
    public DateTime? UpdateDate { get; set; } // Update Date
    public string Updater { get; set; } =string.Empty;// Updated By
    public double? CeilingOpen { get; set; } // Ceiling Open Cost
    public bool? Delete { get; set; } // Deletion Status
}
}
