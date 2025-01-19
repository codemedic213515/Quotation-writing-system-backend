using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/unit")]
public class UnitMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UnitMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitMaster>>> GetUnitMasters()
    {
        return await _context.UnitMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitMaster>> GetUnitMaster(int id)
    {
        var unitMaster = await _context.UnitMasters.FindAsync(id);
        if (unitMaster == null)
        {
            return NotFound();
        }
        return unitMaster;
    }

    [HttpPost]
    public async Task<ActionResult<UnitMaster>> CreateUnitMaster([FromBody] UnitMaster unitMaster)
    {
        _context.UnitMasters.Add(unitMaster);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUnitMaster), new { id = unitMaster.Id }, unitMaster);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUnitMaster(int id, [FromBody] UnitMaster unitMaster)
    {
        if (id != unitMaster.Id)
        {
            return BadRequest();
        }

        _context.Entry(unitMaster).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnitMaster(int id)
    {
        var unitMaster = await _context.UnitMasters.FindAsync(id);
        if (unitMaster == null)
        {
            return NotFound();
        }

        _context.UnitMasters.Remove(unitMaster);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}