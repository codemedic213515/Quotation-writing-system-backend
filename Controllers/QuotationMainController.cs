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

    public QuotationMainController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuotationMain>>> GetQuotationMains()
    {
        return await _context.QuotationMains.ToListAsync();
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
        _context.QuotationMains.Add(quotationMain);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetQuotationMain), new { id = quotationMain.Id }, quotationMain);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuotationMain(int id, [FromBody] QuotationMain quotationMain)
    {
        if (id != quotationMain.Id)
        {
            return BadRequest();
        }

        _context.Entry(quotationMain).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
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