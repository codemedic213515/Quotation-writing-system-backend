using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
    [Route("api/quotationcalc")]
    [ApiController]
    public class QuotationCalcController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public QuotationCalcController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/QuotationCalc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuotationCalc>>> GetQuotationCalcs()
        {
            return await _context.QuotationCalcs.ToListAsync();
        }

        // GET: api/QuotationCalc/5
    [HttpGet("{id}")]
public async Task<ActionResult<QuotationCalc>> GetQuotationCalc(string id)
{
    // Try to fetch the QuotationCalc from the database by ID
    var quotationCalc = await _context.QuotationCalcs
        .Where(q => q.Number == id) // Ensure we are looking by the correct field, like `Number` or `Id`
        .FirstOrDefaultAsync();

    // If no data is found, return a default response with 100% for rates
    if (quotationCalc == null)
    {
        return Ok(new QuotationCalc
        {
            TubeNetRate = 100, // Default value for 電線管
            TubeReplenishmentRate = 100, // Default value for 電線管
            CableNetRate = 100, // Default value for 電線・ケーブル
            CableReplenishmentRate = 100 // Default value for 電線・ケーブル
        });
    }

    // If data is found, return the existing QuotationCalc object
    return Ok(quotationCalc);
}


        // PUT: api/QuotationCalc/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuotationCalc(int id, QuotationCalc quotationCalc)
        {
            if (id != quotationCalc.Id)
            {
                return BadRequest();
            }

            _context.Entry(quotationCalc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuotationCalcExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuotationCalc
 [HttpPost]
public async Task<ActionResult<QuotationCalc>> PostQuotationCalc(QuotationCalc quotationCalc)
{
    // Check if a record with the same Number exists
    var existingQuotation = await _context.QuotationCalcs
        .FirstOrDefaultAsync(q => q.Number == quotationCalc.Number);

    if (existingQuotation != null)
    {
        // Update only the fields that are provided in the incoming request
        if (quotationCalc.TubeNetRate != default)
            existingQuotation.TubeNetRate = quotationCalc.TubeNetRate;

        if (quotationCalc.TubeReplenishmentRate != default)
            existingQuotation.TubeReplenishmentRate = quotationCalc.TubeReplenishmentRate;

        if (quotationCalc.CableNetRate != default)
            existingQuotation.CableNetRate = quotationCalc.CableNetRate;

        if (quotationCalc.CableReplenishmentRate != default)
            existingQuotation.CableReplenishmentRate = quotationCalc.CableReplenishmentRate;

        if (!string.IsNullOrEmpty(quotationCalc.Rank))
            existingQuotation.Rank = quotationCalc.Rank;

        if (quotationCalc.LaborCostA != default)
            existingQuotation.LaborCostA = quotationCalc.LaborCostA;

        if (quotationCalc.LaborCostB != default)
            existingQuotation.LaborCostB = quotationCalc.LaborCostB;

        if (quotationCalc.SiteMiscellRate != default)
            existingQuotation.SiteMiscellRate = quotationCalc.SiteMiscellRate;

        if (quotationCalc.MiscellRate != default)
            existingQuotation.MiscellRate = quotationCalc.MiscellRate;

        // Update boolean fields if necessary
        if (quotationCalc.ABMethod != default)
            existingQuotation.ABMethod = quotationCalc.ABMethod;
        if (quotationCalc.Minority != default)
            existingQuotation.Minority = quotationCalc.Minority;

        // Save changes
        await _context.SaveChangesAsync();

        return Ok(existingQuotation); // Return the updated record
    }
    else
    {
        // If no existing record, add a new one
        _context.QuotationCalcs.Add(quotationCalc);
        await _context.SaveChangesAsync();

        return Ok(quotationCalc); // Return created record
    }
}
        // DELETE: api/QuotationCalc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotationCalc(int id)
        {
            var quotationCalc = await _context.QuotationCalcs.FindAsync(id);
            if (quotationCalc == null)
            {
                return NotFound();
            }

            _context.QuotationCalcs.Remove(quotationCalc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuotationCalcExists(int id)
        {
            return _context.QuotationCalcs.Any(e => e.Id == id);
        }
    }
}
