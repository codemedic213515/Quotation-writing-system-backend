using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/quotationtype")]
public class QuotationTypeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuotationTypeController(ApplicationDbContext context)
    {
        _context = context;
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
        _context.QuotationTypes.Add(quotationType);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuotationType), new { id = quotationType.Id }, quotationType);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuotationType(int id, [FromBody] QuotationType quotationType)
    {
        if (id != quotationType.Id)
        {
            return BadRequest();
        }

        _context.Entry(quotationType).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
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