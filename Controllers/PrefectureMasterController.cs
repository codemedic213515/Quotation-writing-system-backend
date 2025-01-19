using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/prefecture")]
public class PrefectureMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PrefectureMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrefectureMaster>>> GetPrefectureMasters()
    {
        return await _context.PrefectureMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PrefectureMaster>> GetPrefectureMaster(int id)
    {
        var prefecture = await _context.PrefectureMasters.FindAsync(id);
        if (prefecture == null)
        {
            return NotFound();
        }
        return prefecture;
    }

    [HttpPost]
    public async Task<ActionResult<PrefectureMaster>> CreatePrefectureMaster([FromBody] PrefectureMaster prefecture)
    {
        _context.PrefectureMasters.Add(prefecture);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPrefectureMaster), new { id = prefecture.Id }, prefecture);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePrefectureMaster(int id, [FromBody] PrefectureMaster prefecture)
    {
        if (id != prefecture.Id)
        {
            return BadRequest();
        }

        _context.Entry(prefecture).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrefectureMaster(int id)
    {
        var prefecture = await _context.PrefectureMasters.FindAsync(id);
        if (prefecture == null)
        {
            return NotFound();
        }

        _context.PrefectureMasters.Remove(prefecture);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}