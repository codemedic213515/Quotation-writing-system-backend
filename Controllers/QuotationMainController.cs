using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/quotationmain")]
public class QuotationMainController : ControllerBase
{
    private readonly ApplicationDbContext _context;
      private readonly ILogger<QuotationMainController> _logger;
    public QuotationMainController(ApplicationDbContext context, ILogger<QuotationMainController> logger)
        {
            _context = context;
            _logger = logger;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuotationMain>>> GetQuotationMains()
    {
        return await _context.QuotationMains.ToListAsync();
    }
 [HttpGet("count")]
    public async Task<ActionResult<IEnumerable<QuotationMain>>> GetQuotationMainCount()
    {
        try
    {
        var count = await _context.QuotationMains.CountAsync();
        return Ok(  count );
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching QuotationMain count");
        return StatusCode(500, "Internal Server Error");
    }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<QuotationMain>> GetQuotationMain(int id)
    {
        var quotationMain = await _context.QuotationMains.FindAsync(id);
        if (quotationMain == null)
        {
            return NotFound();
        }
        return quotationMain;
    }

    [HttpPost]
    public async Task<ActionResult<QuotationMain>> CreateQuotationMain([FromBody] QuotationMain quotationMain)
    {

         var existingQuotation = await _context.QuotationMains
        .FirstOrDefaultAsync(c => 
            c.Code == quotationMain.Code );

    if (existingQuotation != null)
    {
        // Return 400 Bad Request if a duplicate is found with an appropriate error message
        return BadRequest("A Quotation with the same Code is already exists.");
    }

        _context.QuotationMains.Add(quotationMain);
        await _context.SaveChangesAsync();
        return Ok( quotationMain);
    }

[HttpPut("{code}")]
public async Task<IActionResult> UpdateQuotationMain(string code, [FromBody] QuotationMain quotationMain)
{
    if (code != quotationMain.Code)
    {
        // Return 400 if the provided 'code' in the URL doesn't match the 'code' in the request body
        return BadRequest("Code mismatch between URL and request body.");
    }

    // Fetch the existing QuotationMain by code
    var existingQuotation = await _context.QuotationMains.FirstOrDefaultAsync(q => q.Code == quotationMain.Code);
    if (existingQuotation == null)
    {
        // Return 404 if no existing quotation is found with the provided 'code'
        return NotFound($"Quotation with code {code} not found.");
    }

    // Update only the properties of the existing quotation that need to be changed
    existingQuotation.Purpose = quotationMain.Purpose;
    existingQuotation.Square = quotationMain.Square;
    existingQuotation.Standard = quotationMain.Standard;

    // Update other properties as needed...

    // Save the changes to the database
    await _context.SaveChangesAsync();

    // Return the updated quotation object with a 200 OK status
    return Ok(existingQuotation);
}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuotationMain(int id)
    {
        var quotationMain = await _context.QuotationMains.FindAsync(id);
        if (quotationMain == null)
        {
            return NotFound();
        }

        _context.QuotationMains.Remove(quotationMain);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}