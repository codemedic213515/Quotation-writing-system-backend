using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/quotationtype")]
public class QuotationTypeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
private readonly ILogger<QuotationTypeController> _logger;

    public QuotationTypeController(ApplicationDbContext context, ILogger<QuotationTypeController> logger)
        {
            _context = context;
            _logger = logger;
        }
 [HttpGet]
public async Task<ActionResult<IEnumerable<QuotationType>>> GetQuotationTypes([FromQuery] string number)
{
    // Check if 'number' is provided
    if (string.IsNullOrWhiteSpace(number))
    {
        return BadRequest("number is required.");
    }

    // Find existing quotation types by the given 'number'
    var existingQuotationTypes = await _context.QuotationTypes
        .Where(q => q.Number == number)  // Filter based on the 'number'
        .ToListAsync();

    // If no data is found, return a 404 (Not Found)
    if (existingQuotationTypes.Count == 0)
    {
        return NotFound("No quotation types found for the given number.");
    }

    // Return the filtered list of quotation types
    return Ok(existingQuotationTypes);
}


    [HttpGet("{id}")]
    public async Task<ActionResult<QuotationType>> GetQuotationType(int id)
    {
        var quotationType = await _context.QuotationTypes.FindAsync(id);
        if (quotationType == null)
        {
            return NotFound();
        }
        return quotationType;
    }
  
[HttpPost]
public async Task<ActionResult<QuotationType>> CreateQuotationType([FromBody] QuotationType quotationType)
{
    if (quotationType == null)
    {
        return BadRequest("Invalid quotation type data.");
    }

    _logger.LogInformation("Received quotationType: {@quotationType}", quotationType);

    // Ensure default values if they are not provided
    quotationType.Delete ??= false;  // Default value for Delete
    quotationType.RemovalRate ??= 0.0; // Default value for RemovalRate

    try
    {
        await _context.QuotationTypes.AddAsync(quotationType);
        await _context.SaveChangesAsync();

        return Ok(quotationType);
    }
    catch (Exception ex)
    {
        _logger.LogError("Error saving quotationType: {Message}", ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, 
            new { message = "Internal server error", error = ex.Message });
    }
}




[HttpDelete("{id}")]
public async Task<IActionResult> DeleteQuotationType(int id)
{
    var quotationType = await _context.QuotationTypes.FindAsync(id);
    if (quotationType == null)
    {
        return NotFound();
    }

    _context.QuotationTypes.Remove(quotationType);
    await _context.SaveChangesAsync();

    return NoContent();
}
[HttpPost("save")]
public async Task<IActionResult> SaveQuotationData([FromBody] QuotationSaveRequest request)
{
    if (request == null || string.IsNullOrWhiteSpace(request.Number) || request.CleanedData == null)
    {
        return BadRequest("Invalid request data.");
    }

    try
    {
        // Step 1: Remove Existing Data for the Given Number
        var existingRecords = _context.QuotationTypes.Where(q => q.Number == request.Number);
        _context.QuotationTypes.RemoveRange(existingRecords);
        await _context.SaveChangesAsync(); // Save changes before inserting new records

        // Step 2: Insert New Data
        var newRecords = request.CleanedData.Select(item => new QuotationType
        {
            Number = request.Number,
            Category1 = item.Category1,
            Category2 = item.Category2,
            Category3 = item.Category3,
            Category4 = item.Category4 ?? "", // Default to empty string if null
            RemovalRate = item.RemovalRate ?? 0,
            Delete = false // Default to active
        });

        await _context.QuotationTypes.AddRangeAsync(newRecords);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Data saved successfully!" });
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}
}
}
public class QuotationSaveRequest
{
    public string Number { get; set; }=string.Empty;
    public List<QuotationTypeDto> CleanedData { get; set; }
}

public class QuotationTypeDto
{
    public string Category1 { get; set; }=string.Empty;    public string Category2 { get; set; }=string.Empty;
    public string Category3 { get; set; }=string.Empty;
    public string Category4 { get; set; }=string.Empty;
    public double? RemovalRate { get; set; }
}