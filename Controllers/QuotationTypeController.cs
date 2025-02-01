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


[HttpGet("{Number}")]
public async Task<ActionResult<IEnumerable<QuotationType>>> GetQuotationType(string Number)
{
    // Fetch QuotationTypes by matching the Number field (you can change the query logic as needed)
    var quotationTypes = await _context.QuotationTypes
                                       .Where(q => q.Number.Contains(Number)) // Adjust the query as per your need
                                       .ToListAsync(); // Returns a list of matching QuotationTypes

    if (!quotationTypes.Any()) // Check if no results were found
    {
        return NotFound(); // Return 404 if no results found
    }

    return Ok(quotationTypes); // Return the list of QuotationTypes
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
[HttpPut("{id}")]
public async Task<IActionResult> UpdateQuotationType(int id, [FromBody] QuotationUpdateRequest request)
{


    try
    {
        // Fetch the existing QuotationType by Id
        var quotationType = await _context.QuotationTypes
            .FirstOrDefaultAsync(q => q.Id == id); // We can use FirstOrDefaultAsync here instead of Where

        if (quotationType == null)
        {
            return NotFound("No quotation types found for the given id.");
        }

        // Update specified fields
        if (request.RemovalRate!=default)
        {
            quotationType.RemovalRate = request.RemovalRate;
        }
        if (request.Calculate) // Add validation for 'calculate' if necessary
        {
            quotationType.Calculate = request.Calculate;
        }

        // Save changes to the database
        await _context.SaveChangesAsync();

        return Ok(new { message = "Quotation data updated successfully!" });
    }
    catch (Exception ex)
    {
        _logger.LogError("Error updating quotation data: {Message}", ex.Message);
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}

}
public class QuotationSaveRequest
    {
        public string Number { get; set; } = string.Empty; // Ensure it's initialized
        public List<QuotationTypeDto> CleanedData { get; set; } = new List<QuotationTypeDto>(); // Initialize to avoid null
    }

  public class QuotationTypeDto
    {
        public string Category1 { get; set; } = string.Empty; // Initialize to avoid null
        public string Category2 { get; set; } = string.Empty; // Initialize to avoid null
        public string Category3 { get; set; } = string.Empty; // Initialize to avoid null
        public string Category4 { get; set; } = string.Empty; // Initialize to avoid null
        public double? RemovalRate { get; set; } // Nullable
    } 
  public class QuotationUpdateRequest
    {
        public int RemovalRate { get; set; } // Nullable field for partial update
        public bool Calculate { get; set; } // Nullable field for partial update
       
    }
}
  