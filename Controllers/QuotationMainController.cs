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

// In your controller (e.g., QuotationMainController.cs)
[HttpGet("users")]
public async Task<ActionResult<IEnumerable<string>>> GetUsers()
{
    var users = await _context.QuotationMains
        .Where(q => !string.IsNullOrEmpty(q.Creater)) // Ensure no null or empty usernames
        .Select(q => q.Creater) // Assuming 'Creater' field holds the username
        .Distinct() // Get distinct usernames
        .ToListAsync();

    return Ok(users);
}

   [HttpGet]
  
public async Task<ActionResult> GetQuotationMains(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10, // Default to 10 if not provided
    [FromQuery] string? creater = null,
    [FromQuery] string? code = null,
    [FromQuery] DateTime? createDate = null)
{
    // Base query
    var query = _context.QuotationMains.AsQueryable();

    // Apply filters
    if (!string.IsNullOrWhiteSpace(creater))
    {
        query = query.Where(q => q.Creater.Contains(creater));
    }

    if (!string.IsNullOrWhiteSpace(code))
    {
        query = query.Where(q => q.Code.Contains(code));
    }

    if (createDate.HasValue)
    {
        query = query.Where(q => q.CreatedAt.Date == createDate.Value.Date);
    }

    // Total count before pagination
    var totalRecords = await query.CountAsync();

    // Apply pagination
    var quotationMains = await query
        .OrderBy(q => q.Id) // Order by ID or other relevant field
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    // Return paginated and filtered response
    return Ok(new
    {
        Data = quotationMains,
        TotalRecords = totalRecords,
        Page = page,
        PageSize = pageSize
    });
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
    if (string.IsNullOrWhiteSpace(code) || code != quotationMain.Code)
    {
        // Return 400 if the provided 'code' in the URL doesn't match the 'code' in the request body
        return BadRequest("Code mismatch between URL and request body.");
    }

    // Fetch the existing QuotationMain by code
    var existingQuotation = await _context.QuotationMains.FirstOrDefaultAsync(q => q.Code == code);
    if (existingQuotation == null)
    {
        // Return 404 if no existing quotation is found with the provided 'code'
        return NotFound($"Quotation with code {code} not found.");
    }

    // Update only the properties provided in the request body (partial update)
    if (!string.IsNullOrWhiteSpace(quotationMain.Creater))
        existingQuotation.Creater = quotationMain.Creater;

    if (!string.IsNullOrWhiteSpace(quotationMain.Name))
        existingQuotation.Name = quotationMain.Name;

    if (!string.IsNullOrWhiteSpace(quotationMain.Address))
        existingQuotation.Address = quotationMain.Address;

    if (!string.IsNullOrWhiteSpace(quotationMain.Export))
        existingQuotation.Export = quotationMain.Export;

    if (!string.IsNullOrWhiteSpace(quotationMain.Import))
        existingQuotation.Import = quotationMain.Import;

    if (!string.IsNullOrWhiteSpace(quotationMain.Purpose))
        existingQuotation.Purpose = quotationMain.Purpose;

    if (!string.IsNullOrWhiteSpace(quotationMain.Square))
        existingQuotation.Square = quotationMain.Square;

    if (!string.IsNullOrWhiteSpace(quotationMain.Standard))
        existingQuotation.Standard = quotationMain.Standard;
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