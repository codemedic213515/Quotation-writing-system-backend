using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QuotationWritingSystem.Controllers
{
    [ApiController]
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

        // Get distinct users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<string>>> GetUsers()
        {
            try
            {
                var users = await _context.QuotationMains
                    .Where(q => !string.IsNullOrEmpty(q.Creater)) // Ensure no null or empty usernames
                    .Select(q => q.Creater) // Assuming 'Creater' field holds the username
                    .Distinct() // Get distinct usernames
                    .ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching users");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get paginated and filtered quotations
        [HttpGet]
        public async Task<ActionResult> GetQuotationMains(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10, // Default to 10 if not provided
            [FromQuery] string? creater = null,
            [FromQuery] string? code = null,
            [FromQuery] DateTime? createDate = null)
        {
            try
            {
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

                return Ok(new
                {
                    Data = quotationMains,
                    TotalRecords = totalRecords,
                    Page = page,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching quotations");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get the count of quotations
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetQuotationMainCount()
        {
            try
            {
                var count = await _context.QuotationMains.CountAsync();
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching QuotationMain count");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Get a specific quotation by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<QuotationMain>> GetQuotationMain(int id)
        {
            try
            {
                var quotationMain = await _context.QuotationMains.FindAsync(id);
                if (quotationMain == null)
                {
                    return NotFound();
                }
                return Ok(quotationMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching QuotationMain with ID {Id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Create a new quotation
        [HttpPost]
        public async Task<ActionResult<QuotationMain>> CreateQuotationMain([FromBody] QuotationMain quotationMain)
        {
            try
            {
                var existingQuotation = await _context.QuotationMains
                    .FirstOrDefaultAsync(c => c.Code == quotationMain.Code);

                if (existingQuotation != null)
                {
                    return BadRequest("A Quotation with the same Code already exists.");
                }

                _context.QuotationMains.Add(quotationMain);
                await _context.SaveChangesAsync();

                return Ok( quotationMain);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new QuotationMain");
                return StatusCode(500, "Internal Server Error");
            }
        }

        // Update an existing quotation by Code
        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateQuotationMain(string code, [FromBody] QuotationMain data)
        {_logger.LogInformation("aa:{data}",data);
            try
            {
                if (string.IsNullOrWhiteSpace(code) || code != data.Code)
                {
                    return BadRequest("Code mismatch between URL and request body.");
                }

                var existingQuotation = await _context.QuotationMains.FirstOrDefaultAsync(q => q.Code == code);
                if (existingQuotation == null)
                {
                    return NotFound($"Quotation with code {code} not found.");
                }

                // Update the existing quotation
                if (!string.IsNullOrWhiteSpace(data.Creater))
                    existingQuotation.Creater = data.Creater;

                if (!string.IsNullOrWhiteSpace(data.Name))
                    existingQuotation.Name = data.Name;

                if (!string.IsNullOrWhiteSpace(data.Address))
                    existingQuotation.Address = data.Address;

                if (!string.IsNullOrWhiteSpace(data.Export))
                    existingQuotation.Export = data.Export;

                if (!string.IsNullOrWhiteSpace(data.Import))
                    existingQuotation.Import = data.Import;

                if (!string.IsNullOrWhiteSpace(data.Purpose))
                    existingQuotation.Purpose = data.Purpose;

                if (!string.IsNullOrWhiteSpace(data.Square))
                    existingQuotation.Square = data.Square;

                if (!string.IsNullOrWhiteSpace(data.Standard))
                    existingQuotation.Standard = data.Standard;

                await _context.SaveChangesAsync();

                return Ok(existingQuotation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating QuotationMain with code {Code}", code);
                return StatusCode(500, ex);
            }
        }

        // Delete a quotation by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuotationMain(int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting QuotationMain with ID {Id}", id);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
