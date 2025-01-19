using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/year")]
public class YearMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public YearMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<YearMaster>>> GetYearMasters()
    {
        return await _context.YearMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<YearMaster>> GetYearMaster(int id)
    {
        var yearMaster = await _context.YearMasters.FindAsync(id);
        if (yearMaster == null)
        {
            return NotFound();
        }
        return yearMaster;
    }

    [HttpPost]
    public async Task<ActionResult<YearMaster>> CreateYearMaster([FromBody] YearMaster yearMaster)
    {
        _context.YearMasters.Add(yearMaster);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetYearMaster), new { id = yearMaster.Id }, yearMaster);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateYearMaster(int id, [FromBody] YearMaster yearMaster)
    {
        if (id != yearMaster.Id)
        {
            return BadRequest();
        }

        _context.Entry(yearMaster).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteYearMaster(int id)
    {
        var yearMaster = await _context.YearMasters.FindAsync(id);
        if (yearMaster == null)
        {
            return NotFound();
        }

        _context.YearMasters.Remove(yearMaster);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}