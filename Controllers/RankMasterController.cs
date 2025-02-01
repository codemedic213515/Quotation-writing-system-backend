using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/rank")]
public class RankMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RankMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RankMaster>>> GetRankMasters()
    {
        return await _context.RankMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RankMaster>> GetRankMaster(int id)
    {
        var rankMaster = await _context.RankMasters.FindAsync(id);
        if (rankMaster == null)
        {
            return NotFound();
        }

        return rankMaster;
    }

    [HttpPost]
    public async Task<ActionResult<RankMaster>> CreateRankMaster([FromBody] RankMaster rankMaster)
    {
        _context.RankMasters.Add(rankMaster);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRankMaster), new { id = rankMaster.Id }, rankMaster);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRankMaster(int id, [FromBody] RankMaster rankMaster)
    {
        if (id != rankMaster.Id)
        {
            return BadRequest();
        }

        _context.Entry(rankMaster).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(rankMaster);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRankMaster(int id)
    {
        var rankMaster = await _context.RankMasters.FindAsync(id);
        if (rankMaster == null)
        {
            return NotFound();
        }

        _context.RankMasters.Remove(rankMaster);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}