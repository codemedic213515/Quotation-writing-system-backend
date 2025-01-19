using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/construction")]
public class ConstructionMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ConstructionMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConstructionMaster>>> GetConstructionMasters()
    {
        return await _context.ConstructionMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ConstructionMaster>> GetConstructionMaster(int id)
    {
        var construction = await _context.ConstructionMasters.FindAsync(id);
        if (construction == null)
        {
            return NotFound();
        }
        return construction;
    }

    [HttpPost]
    public async Task<ActionResult<ConstructionMaster>> CreateConstructionMaster([FromBody] ConstructionMaster construction)
    {
        _context.ConstructionMasters.Add(construction);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetConstructionMaster), new { id = construction.Id }, construction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConstructionMaster(int id, [FromBody] ConstructionMaster construction)
    {
        if (id != construction.Id)
        {
            return BadRequest();
        }

        _context.Entry(construction).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConstructionMaster(int id)
    {
        var construction = await _context.ConstructionMasters.FindAsync(id);
        if (construction == null)
        {
            return NotFound();
        }

        _context.ConstructionMasters.Remove(construction);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}