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
        // Fetch materials related to the quotation
        var materials = await _context.QuotationMaterials
            .Where(q => _context.QuotationTypes
                .Any(t => t.Number == quotationNumber && t.Id == q.TypeId))
            .ToListAsync();

        if (!materials.Any())
            return new { Message = "No materials found for the given quotation." };

        // Fetch RankMaster to get dynamic rates
        var rankMaster = await _context.RankMasters.FirstOrDefaultAsync(r => r.Id == 2); // Modify as needed

        if (rankMaster == null)
            return new { Message = "No RankMaster data found." };

        // Dynamic rates from RankMaster
        // Dynamic rates from RankMaster
        decimal laborRate = (decimal)(rankMaster.LaborCostA ?? 0);
        decimal overheadRate = (decimal)(rankMaster.SiteMiscell ?? 0) / 100;  // Convert % to decimal
        decimal miscellaneousRate = (decimal)(rankMaster.OtherExpens ?? 0) / 100;  // Convert % to decimal
        decimal generalExpensesRate = 0.1M; // Assuming 10% general expenses


        // Calculate costs using database values
        // Convert string values to decimal before calculations
        decimal materialCost = materials.Sum(m => 
            Convert.ToDecimal(m.Quantity) * Convert.ToDecimal(m.Price) * Convert.ToDecimal(m.StepRate)
        );

        decimal laborCost = materials.Sum(m => 
            Convert.ToDecimal(m.Quantity) * Convert.ToDecimal(m.StepRate) * laborRate
        );

        decimal overheadCost = materialCost * overheadRate;
        decimal miscellaneousCost = materialCost * miscellaneousRate;
        decimal generalExpenses = (materialCost + laborCost + overheadCost) * generalExpensesRate;
        decimal totalProposalCost = materialCost + laborCost + overheadCost + miscellaneousCost + generalExpenses;

        // Return computed values
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
        // Fetch QuotationTypes related to the given quotationNumber
        var quotationTypes = await _context.QuotationTypes
            .Where(qt => qt.Number == quotationNumber)
            .ToListAsync();

        if (!quotationTypes.Any())
            return new List<object> { new { Message = "No QuotationTypes found for this quotation number." } };

        var resultList = new List<object>();

        foreach (var quotationType in quotationTypes)
        {
            // Fetch materials related to this QuotationType
            var materials = await _context.QuotationMaterials
                .Where(qm => qm.TypeId == quotationType.Id)
                .ToListAsync();

            if (!materials.Any())
                continue;

            // Fetch RankMaster for cost rates
            var rankMaster = await _context.RankMasters.FirstOrDefaultAsync(r => r.Id == 2); // Adjust Rank ID if needed

            if (rankMaster == null)
                return new List<object> { new { Message = "No RankMaster data found." } };

            // Dynamic cost rates from RankMaster
            decimal laborRate = (decimal)(rankMaster.LaborCostA ?? 0);
            decimal overheadRate = (decimal)(rankMaster.SiteMiscell ?? 0) / 100;  // Convert % to decimal
            decimal miscellaneousRate = (decimal)(rankMaster.OtherExpens ?? 0) / 100;  // Convert % to decimal
            decimal generalExpensesRate = 0.1M; // Assuming 10% general expenses

            // Calculate costs for the current QuotationType
            decimal materialCost = materials.Sum(m =>
                Convert.ToDecimal(m.Quantity) * Convert.ToDecimal(m.Price) * Convert.ToDecimal(m.StepRate)
            );

            decimal laborCost = materials.Sum(m =>
                Convert.ToDecimal(m.Quantity) * Convert.ToDecimal(m.StepRate) * laborRate
            );

            decimal overheadCost = materialCost * overheadRate;
            decimal miscellaneousCost = materialCost * miscellaneousRate;
            decimal generalExpenses = (materialCost + laborCost + overheadCost) * generalExpensesRate;
            decimal totalProposalCost = materialCost + laborCost + overheadCost + miscellaneousCost + generalExpenses;

            // Add to the list
            resultList.Add(new
            {
                QuotationTypeId = quotationType.Id,
                Category1 = quotationType.Category1,
                Category2 = quotationType.Category2,
                Category3 = quotationType.Category3,
                MaterialCost = materialCost,
                LaborCost = laborCost,
                OverheadCost = overheadCost,
                MiscellaneousCost = miscellaneousCost,
                GeneralExpenses = generalExpenses,
                TotalProposalCost = totalProposalCost
            });
        }

        return resultList;
    }
    catch (Exception ex)
    {
        return new List<object> { new { Message = "An error occurred while calculating costs.", Error = ex.Message } };
    }
}

}
