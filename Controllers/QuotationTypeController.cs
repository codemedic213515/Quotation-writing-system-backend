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
    public async Task<ActionResult<IEnumerable<QuotationType>>> GetQuotationTypes()
    {
        return await _context.QuotationTypes.ToListAsync();
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
}
}