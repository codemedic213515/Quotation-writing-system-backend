using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using System.Linq;
using System.Threading.Tasks;

public class QuotationService
{
    private readonly ApplicationDbContext _context;
    public QuotationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> CalculateQuotationCost(string quotationNumber)
    {
        var materials = await _context.QuotationMaterials
            .Where(q => _context.QuotationTypes
                .Any(t => t.Number == quotationNumber && t.Id == q.TypeId))
            .ToListAsync();

        if (!materials.Any())
            return new { Message = "No materials found for the given quotation." };

        var rankMaster = await _context.RankMasters.FirstOrDefaultAsync(r => r.Id == 2);
        if (rankMaster == null)
            return new { Message = "No RankMaster data found." };

        decimal laborRate = Convert.ToDecimal(rankMaster.LaborCostA ?? 0);
        decimal overheadRate = Convert.ToDecimal(rankMaster.OtherExpens ?? 0) / 100;
        decimal miscellaneousRate = Convert.ToDecimal(rankMaster.SiteMiscell ?? 0) / 100;
        decimal generalExpensesRate = 0.1M;

        decimal materialCost = materials.Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.Price) * SafeDecimal(m.StepRate));
        decimal laborCost = materials.Sum(m => SafeDecimal(m.Quantity) * SafeDecimal(m.StepRate) * laborRate);
        decimal overheadCost = materialCost * overheadRate;
        decimal miscellaneousCost = materialCost * miscellaneousRate;
        decimal generalExpenses = (materialCost + laborCost + overheadCost) * generalExpensesRate;
        decimal totalProposalCost = materialCost + laborCost + overheadCost + miscellaneousCost + generalExpenses;

        return new
        {
            MaterialCost = materialCost,
            LaborCost = laborCost,
            OverheadCost = overheadCost,
            MiscellaneousCost = miscellaneousCost,
            GeneralExpenses = generalExpenses,
            TotalProposalCost = totalProposalCost
        };
    }

    public async Task<List<object>> GetCostsForEachQuotationType(string quotationNumber)
    {
        try
        {
            var quotationTypes = await _context.QuotationTypes
                .Where(qt => qt.Number == quotationNumber)
                .ToListAsync();
            var quotationCalc = await _context.QuotationCalcs
                .Where(qc => qc.Number == quotationNumber)
                .FirstOrDefaultAsync();
                
            if (!quotationTypes.Any())
                return new List<object> { new { Message = "No QuotationTypes found for this quotation number." } };

            var resultList = new List<object>();

            foreach (var quotationType in quotationTypes)
            {
                var materials = await _context.QuotationMaterials
                    .Where(qm => qm.TypeId == quotationType.Id)
                    .ToListAsync();

                if (!materials.Any())
                    continue;

                decimal laborRate = Convert.ToDecimal(quotationCalc?.LaborCostA );
        decimal overheadRate = Convert.ToDecimal(quotationCalc?.MiscellRate ) / 100;
        decimal miscellaneousRate = Convert.ToDecimal(quotationCalc?.SiteMiscellRate ) / 100;
                decimal generalExpensesRate = 0.1M;

                var materialDetails = materials.Select(m => new
                {
                    Category1 = m.Category1 ?? "",
                    Category2 = m.Category2 ?? "",
                    Category3 = m.Category3 ?? "",
                    Unit = m.Unit ?? "",
                    Quantity = SafeDecimal(m.Quantity),
                    Price = SafeDecimal(m.Price),
                    Amount = SafeDecimal(m.Quantity) * SafeDecimal(m.Price),
                    StepRate = SafeDecimal(m.StepRate),
                }).ToList();

                decimal materialCost = materialDetails.Sum(m => m.Quantity * m.Price * m.StepRate);
                decimal laborCost = materialDetails.Sum(m => m.Quantity * m.StepRate * laborRate);
                decimal overheadCost = materialCost * overheadRate;
                decimal miscellaneousCost = materialCost * miscellaneousRate;
                decimal generalExpenses = (materialCost + laborCost + overheadCost) * generalExpensesRate;
                decimal totalProposalCost = materialCost + laborCost + overheadCost + miscellaneousCost + generalExpenses;

                resultList.Add(new
                {
                    QuotationTypeId = quotationType.Id,
                    Category1 = quotationType.Category1 ?? "",
                    Category2 = quotationType.Category2 ?? "",
                    Category3 = quotationType.Category3 ?? "",
                    MaterialCost = materialCost,
                    LaborCost = laborCost,
                    OverheadCost = overheadCost,
                    MiscellaneousCost = miscellaneousCost,
                    GeneralExpenses = generalExpenses,
                    TotalProposalCost = totalProposalCost,
                    Materials = materialDetails
                });
            }

            return resultList;
        }
        catch (Exception ex)
        {
            return new List<object> { new { Message = "An error occurred while calculating costs.", Error = ex.Message } };
        }
    }

    private decimal SafeDecimal(object value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return 0;
        }

        decimal result;
        return decimal.TryParse(value.ToString(), out result) ? result : 0;
    }

    public async Task<List<object>> GetFormattedQuotationQuotate(string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return new List<object>();
        }

        var quotationTypes = await _context.QuotationTypes
            .Where(q => q.Number == quotationNumber)
            .ToListAsync();
        var quotationCalc = await _context.QuotationCalcs
                .Where(qc => qc.Number == quotationNumber)
                .FirstOrDefaultAsync();
                    decimal miscellRate = Convert.ToDecimal(quotationCalc?.MiscellRate ) / 100;
                    decimal siteMiscellRate = Convert.ToDecimal(quotationCalc?.SiteMiscellRate ) / 100;
  
        if (!quotationTypes.Any())
        {
            return new List<object> { new { Message = "No data" } };
        }

        var formattedData = new List<object>();
        int typeIndex = 1;

        foreach (var type in quotationTypes)
        {
            string categoryName = !string.IsNullOrEmpty(type.Category3) ? type.Category3 :
                                  !string.IsNullOrEmpty(type.Category2) ? type.Category2 :
                                  type.Category1;
            decimal total = 0;
            formattedData.Add(new { category = $"{typeIndex}.* {categoryName}", isHeader = true });

            var materials = await _context.QuotationMaterials
                .Where(m => m.TypeId == type.Id)
                .OrderBy(m => m.Id)
                .ToListAsync();

            decimal subtotal = 0;

            foreach (var material in materials)
            {
                var abMaterial = await _context.ABMaterialMaster.FirstOrDefaultAsync(a => a.Name == material.Category);
                if (abMaterial?.ABCode == "2")
                {

                    decimal quantity = decimal.TryParse(material.Quantity, out decimal q) ? q : 0;
                    decimal price = decimal.TryParse(material.Price, out decimal p) ? p : 0;
                    decimal stepRate = decimal.TryParse(material.StepRate, out decimal sr) ? sr : 1; // Default stepRate to 1 if null
                    decimal calculatedQuantity = quantity * stepRate;
                    decimal amount = quantity * price;

                    formattedData.Add(new
                    {
                        item = material.Category,
                        subItem = (material.Category2 ?? "") + (material.Category3 ?? ""),
                        unit = material.Unit,
                        quantity = calculatedQuantity,
                        unitPrice = price,
                        amount = amount,
                        notes = "",
                        indent = ""
                    });

                    subtotal += amount; // Use the correctly calculated amount
                       total += amount;         
                            }
                        }
            formattedData.Add(new { isSubtotal = true, label = "【 機材費 小 計 】", amount = subtotal, subtotalType = "first" });
            subtotal = 0;

            foreach (var material in materials)
            {
                var abMaterial = await _context.ABMaterialMaster.FirstOrDefaultAsync(a => a.Name == material.Category);
                if (abMaterial?.ABCode == "1")
                {
                    decimal quantity = decimal.TryParse(material.Quantity, out decimal q) ? q : 0;
                    decimal price = decimal.TryParse(material.Price, out decimal p) ? p : 0;
                    decimal stepRate = decimal.TryParse(material.StepRate, out decimal sr) ? sr : 1; // Default stepRate to 1 if null
                    decimal calculatedQuantity = quantity * stepRate;
                    decimal amount = quantity * price;

                    formattedData.Add(new
                    {
                        item = material.Category,
                        subItem = (material.Category2 ?? "") + (material.Category3 ?? ""),
                        unit = material.Unit,
                        quantity = calculatedQuantity,
                        unitPrice = price,
                        amount = amount,
                        notes = "",
                        indent = ""
                    });

                    subtotal += amount; // Use the correctly calculated amount
                    total += amount; 
                }
            }
            formattedData.Add(new{
                item = "現場雑費",
                subItem = "",
                unit = "式",
                quantity = 1,
                unitPrice = siteMiscellRate*total,
                amount = siteMiscellRate*total,
                notes = $"{siteMiscellRate*100}%",
                indent = ""
            });
               formattedData.Add(new{
                item = "諸経費",
                subItem = "",
                unit = "式",
                quantity = 1,
                unitPrice = miscellRate*total,
                amount = miscellRate*total,
                notes = $"{miscellRate*100}%",
                indent = ""
            });
            formattedData.Add(new { isSubtotal = true, label = "【 労務･経費 小 計 】", amount = subtotal + miscellRate*total+siteMiscellRate*total, subtotalType = "second" });
            formattedData.Add(new { isTotal = true, label = $"【 {categoryName} 合 計 】", unit = "１式", amount = total*(1+siteMiscellRate+miscellRate)});
            
            typeIndex++;
        }

        return formattedData;
    }
    public async Task<List<object>> GetFormattedQuotationNet(string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return new List<object>();
        }

        var quotationTypes = await _context.QuotationTypes
            .Where(q => q.Number == quotationNumber)
            .ToListAsync();
        var quotationCalc = await _context.QuotationCalcs
                .Where(qc => qc.Number == quotationNumber)
                .FirstOrDefaultAsync();
                    decimal miscellRate = Convert.ToDecimal(quotationCalc?.MiscellRate ) / 100;
                    decimal siteMiscellRate = Convert.ToDecimal(quotationCalc?.SiteMiscellRate ) / 100;
  
        if (!quotationTypes.Any())
        {
            return new List<object> { new { Message = "No data" } };
        }

        var formattedData = new List<object>();
        int typeIndex = 1;

        foreach (var type in quotationTypes)
        {
            string categoryName = !string.IsNullOrEmpty(type.Category3) ? type.Category3 :
                                  !string.IsNullOrEmpty(type.Category2) ? type.Category2 :
                                  type.Category1;
            decimal total = 0;
            formattedData.Add(new { category = $"{typeIndex}.* {categoryName}", isHeader = true });

            var materials = await _context.QuotationMaterials
                .Where(m => m.TypeId == type.Id)
                .OrderBy(m => m.Id)
                .ToListAsync();

            decimal subtotal = 0;

            foreach (var material in materials)
            {
                var abMaterial = await _context.ABMaterialMaster.FirstOrDefaultAsync(a => a.Name == material.Category);
                var impPrice = await _context.MaterialMasters
                .FirstOrDefaultAsync(a => a.CategoryNam == material.Category && a.Name == material.Category3);
                if (abMaterial?.ABCode == "2")
                {
                    decimal quantity = decimal.TryParse(material.Quantity, out decimal q) ? q : 0;
                    decimal price = Convert.ToDecimal(impPrice?.InternalCos ?? 0);
                    decimal stepRate = decimal.TryParse(material.StepRate, out decimal sr) ? sr : 1; // Default stepRate to 1 if null
                    decimal calculatedQuantity = quantity * stepRate;
                    decimal amount = quantity * price;
                    formattedData.Add(new
                    {
                        item = material.Category,
                        subItem = (material.Category2 ?? "") + (material.Category3 ?? ""),
                        unit = material.Unit,
                        quantity = calculatedQuantity,
                        unitPrice = price,
                        amount = amount,
                        notes = "",
                        indent = ""
                    });

                    subtotal += amount; // Use the correctly calculated amount
                       total += amount;         
                            }
                        }
            formattedData.Add(new { isSubtotal = true, label = "【 機材費 小 計 】", amount = subtotal, subtotalType = "first" });
            subtotal = 0;

            foreach (var material in materials)
            {
                var abMaterial = await _context.ABMaterialMaster.FirstOrDefaultAsync(a => a.Name == material.Category);
                var impPrice = await _context.MaterialMasters
                .FirstOrDefaultAsync(a => a.CategoryNam == material.Category && a.Name == material.Category3);
                if (abMaterial?.ABCode == "1")
                {
                    decimal quantity = decimal.TryParse(material.Quantity, out decimal q) ? q : 0;
                    decimal price = Convert.ToDecimal(impPrice?.InternalCos ?? 0);
                    decimal stepRate = decimal.TryParse(material.StepRate, out decimal sr) ? sr : 1; // Default stepRate to 1 if null
                    decimal calculatedQuantity = quantity * stepRate;
                    decimal amount = quantity * price;

                    formattedData.Add(new
                    {
                        item = material.Category,
                        subItem = (material.Category2 ?? "") + (material.Category3 ?? ""),
                        unit = material.Unit,
                        quantity = calculatedQuantity,
                        unitPrice = price,
                        amount = amount,
                        notes = "",
                        indent = ""
                    });

                    subtotal += amount; // Use the correctly calculated amount
                    total += amount; 
                }
            }
            formattedData.Add(new{
                item = "現場雑費",
                subItem = "",
                unit = "式",
                quantity = 1,
                unitPrice = siteMiscellRate*total,
                amount = siteMiscellRate*total,
                notes = $"{siteMiscellRate*100}%",
                indent = ""
            });
               formattedData.Add(new{
                item = "諸経費",
                subItem = "",
                unit = "式",
                quantity = 1,
                unitPrice = miscellRate*total,
                amount = miscellRate*total,
                notes = $"{miscellRate*100}%",
                indent = ""
            });
            formattedData.Add(new { isSubtotal = true, label = "【 労務･経費 小 計 】", amount = subtotal + miscellRate*total+siteMiscellRate*total, subtotalType = "second" });
            formattedData.Add(new { isTotal = true, label = $"【 {categoryName} 合 計 】", unit = "１式", amount = total*(1+siteMiscellRate+miscellRate)});
            
            typeIndex++;
        }

        return formattedData;
    }
    
}
