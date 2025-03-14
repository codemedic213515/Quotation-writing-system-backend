using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
[Route("api/quotationdata")]
[ApiController]
public class QuotationDataController : ControllerBase

{
    private readonly QuotationService _quotationService;
    private readonly ApplicationDbContext _context;
    public QuotationDataController(ApplicationDbContext context, QuotationService quotationService)
    {
        _context = context;
        _quotationService = quotationService;
    }


    [HttpGet("formatted")]
    public async Task<IActionResult> GetFormattedQuotationQuotate([FromQuery] string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return BadRequest(new { Message = "Quotation number is required." });
        }

        var result = await _quotationService.GetFormattedQuotationQuotate(quotationNumber);

        if (result == null || !result.Any())
        {
            return NotFound(new { Message = "No data" });
        }

        return Ok(result);
    }

    [HttpGet("net")]
    public async Task<IActionResult> GetFormattedQuotationNet([FromQuery] string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return BadRequest(new { Message = "Quotation number is required." });
        }

        var result = await _quotationService.GetFormattedQuotationNet(quotationNumber);

        if (result == null || !result.Any())
        {
            return NotFound(new { Message = "No data" });
        }

        return Ok(result);
    }

[HttpGet("summary")]
public async Task<IActionResult> CalculateQuotationSummary([FromQuery] string quotationNumber)
{
    if (string.IsNullOrEmpty(quotationNumber))
    {
        return BadRequest(new { Message = "Quotation number is required." });
    }

    var quotationTypes = await _context.QuotationTypes
        .Where(qt => qt.Number == quotationNumber)
        .ToListAsync();

    if (!quotationTypes.Any())
    {
        return NotFound(new { Message = "No quotation types found for the given quotation number." });
    }

    var quotationTypeIds = quotationTypes.Select(qt => qt.Id).ToList();
    var materials = await _context.QuotationMaterials
        .Where(qm => qm.TypeId.HasValue && quotationTypeIds.Contains(qm.TypeId.Value))
        .ToListAsync();

    if (!materials.Any())
    {
        return NotFound(new { Message = "No materials found for the given quotation." });
    }

    var categoryNames = new List<string> { "諸経費", "現場雑費", "共 通 費", "付帯工事", "運搬雑費", "外 注 費", "電工労務費" };
    
    decimal totalMaterialCost = materials.Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * SafeDecimal(m.Price));

    var categorySums = categoryNames
        .Select(cn => new
        {
            Category = cn,
            Sum = materials
                .Where(m => m.Category == cn)
                .Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * SafeDecimal(m.Price))
        })
        .ToList();

    var abMaterials = await _context.ABMaterialMaster
        .Where(ab => ab.ABCode == "1" || ab.ABCode == "2")
        .ToListAsync();

    // Use Dictionary to accumulate AB Material sums correctly
    var abMaterialSums = new Dictionary<string, decimal>
    {
        { "1", 0 },
        { "2", 0 }
    };

    foreach (var abMaterial in abMaterials)
    {
        var sum = materials
            .Where(m => m.Category == abMaterial.Name)
            .Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * SafeDecimal(m.Price));

        if (abMaterialSums.ContainsKey(abMaterial.ABCode))
        {
            abMaterialSums[abMaterial.ABCode] += sum;
        }
    }

    var quotationTypeDetails = quotationTypes
        .Select(qt => new
        {
            QuotationType = !string.IsNullOrEmpty(qt.Category3) ? qt.Category3 :
                            !string.IsNullOrEmpty(qt.Category2) ? qt.Category2 :
                            qt.Category1,
            TypeMaterialCost = materials
                .Where(m => m.TypeId == qt.Id)
                .Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * SafeDecimal(m.Price)),
            CategorySums = categoryNames
                .Select(cn => new
                {
                    Category = cn,
                    Sum = materials
                        .Where(m => m.TypeId == qt.Id && m.Category == cn)
                        .Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * SafeDecimal(m.Price))
                })
                .ToList(),
            ABMaterialSums = abMaterials
                .Select(abMaterial => new
                {
                    ABCode = abMaterial.ABCode,
                    Sum = materials
                        .Where(m => m.TypeId == qt.Id && m.Category == abMaterial.Name)
                        .Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * SafeDecimal(m.Price))
                })
                .ToList()
        })
        .ToList();

    return Ok(new
    {
        QuotationNumber = quotationNumber,
        TotalMaterialCost = totalMaterialCost,
        CategorySums = categorySums,
        ABMaterialSums = abMaterialSums.Select(kv => new { ABCode = kv.Key, Sum = kv.Value }).ToList(),
        QuotationTypeDetails = quotationTypeDetails
    });
}

[HttpGet("sumimp")]
public async Task<IActionResult> CalculateQuotationImp([FromQuery] string quotationNumber)
{
    if (string.IsNullOrEmpty(quotationNumber))
    {
        return BadRequest(new { Message = "Quotation number is required." });
    }

    var quotationTypes = await _context.QuotationTypes
        .Where(qt => qt.Number == quotationNumber)
        .ToListAsync();

    if (!quotationTypes.Any())
    {
        return NotFound(new { Message = "No quotation types found for the given quotation number." });
    }

    var quotationTypeIds = quotationTypes.Select(qt => qt.Id).ToList();
    var materials = await _context.QuotationMaterials
        .Where(qm => qm.TypeId.HasValue && quotationTypeIds.Contains(qm.TypeId.Value))
        .ToListAsync();

    if (!materials.Any())
    {
        return NotFound(new { Message = "No materials found for the given quotation." });
    }

    // Fetch MaterialMasters and handle duplicate keys
    var materialMasters = await _context.MaterialMasters
        .GroupBy(mm => new { mm.CategoryNam, mm.Name })
        .ToDictionaryAsync(g => g.Key, g => g.FirstOrDefault());

    var categoryNames = new List<string> { "諸経費", "現場雑費", "共 通 費", "付帯工事", "運搬雑費", "外 注 費", "電工労務費" };

    decimal totalMaterialCost = materials.Sum(m =>
    {
        var materialMasterKey = new { CategoryNam = m.Category, Name = m.Category3 };
        decimal price = materialMasters.ContainsKey(materialMasterKey)
            ? SafeDecimal(materialMasters[materialMasterKey]?.InternalCos??0)
            : 0;
        return SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * price;
    });

    var categorySums = categoryNames
        .Select(cn => new
        {
            Category = cn,
            Sum = materials
                .Where(m => m.Category == cn)
                .Sum(m =>
                {
                    var materialMasterKey = new { CategoryNam = m.Category, Name = m.Category3 };
                    decimal price = materialMasters.ContainsKey(materialMasterKey)
                        ? SafeDecimal(materialMasters[materialMasterKey]?.InternalCos??0)
                        : 0;
                    return SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * price;
                })
        })
        .ToList();

    var abMaterials = await _context.ABMaterialMaster
        .Where(ab => ab.ABCode == "1" || ab.ABCode == "2")
        .ToListAsync();

    // Use Dictionary to accumulate AB Material sums correctly
    var abMaterialSums = new Dictionary<string, decimal>
    {
        { "1", 0 },
        { "2", 0 }
    };

    foreach (var abMaterial in abMaterials)
    {
        var sum = materials
            .Where(m => m.Category == abMaterial.Name)
            .Sum(m =>
            {
                var materialMasterKey = new { CategoryNam = m.Category, Name = m.Category3 };
                decimal price = materialMasters.ContainsKey(materialMasterKey)
                    ? SafeDecimal(materialMasters[materialMasterKey]?.InternalCos??0)
                    : 0;
                return SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * price;
            });

        if (abMaterialSums.ContainsKey(abMaterial.ABCode))
        {
            abMaterialSums[abMaterial.ABCode] += sum;
        }
    }

    var quotationTypeDetails = quotationTypes
        .Select(qt => new
        {
            QuotationType = !string.IsNullOrEmpty(qt.Category3) ? qt.Category3 :
                            !string.IsNullOrEmpty(qt.Category2) ? qt.Category2 :
                            qt.Category1,
            TypeMaterialCost = materials
                .Where(m => m.TypeId == qt.Id)
                .Sum(m =>
                {
                    var materialMasterKey = new { CategoryNam = m.Category, Name = m.Category3 };
                    decimal price = materialMasters.ContainsKey(materialMasterKey)
                        ? SafeDecimal(materialMasters[materialMasterKey]?.InternalCos??0)
                        : 0;
                    return SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * price;
                }),
            CategorySums = categoryNames
                .Select(cn => new
                {
                    Category = cn,
                    Sum = materials
                        .Where(m => m.TypeId == qt.Id && m.Category == cn)
                        .Sum(m =>
                        {
                            var materialMasterKey = new { CategoryNam = m.Category, Name = m.Category3 };
                            decimal price = materialMasters.ContainsKey(materialMasterKey)
                                ? SafeDecimal(materialMasters[materialMasterKey]?.InternalCos??0)
                                : 0;
                            return SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * price;
                        })
                })
                .ToList(),
            ABMaterialSums = abMaterials
                .Select(abMaterial => new
                {
                    ABCode = abMaterial.ABCode,
                    Sum = materials
                        .Where(m => m.TypeId == qt.Id && m.Category == abMaterial.Name)
                        .Sum(m =>
                        {
                            var materialMasterKey = new { CategoryNam = m.Category, Name = m.Category3 };
                            decimal price = materialMasters.ContainsKey(materialMasterKey)
                                ? SafeDecimal(materialMasters[materialMasterKey]?.InternalCos??0)
                                : 0;
                            return SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * price;
                        })
                })
                .ToList()
        })
        .ToList();

    return Ok(new
    {
        QuotationNumber = quotationNumber,
        TotalMaterialCost = totalMaterialCost,
        CategorySums = categorySums,
        ABMaterialSums = abMaterialSums.Select(kv => new { ABCode = kv.Key, Sum = kv.Value }).ToList(),
        QuotationTypeDetails = quotationTypeDetails
    });
}private decimal SafeDecimal(object value)
{
    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
    {
        return 0;
    }
    decimal result;
    return decimal.TryParse(value.ToString(), out result) ? result : 0;
}
}
