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
 [HttpGet("masterdata")]
        public async Task<ActionResult<object>> GetConstructions([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var query = _context.ConstructionMasters.AsQueryable();
                var totalRecords = await query.CountAsync();
                var constructions = await query
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return Ok(new { constructions, total = totalRecords });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error. Please try again later.");
            }
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