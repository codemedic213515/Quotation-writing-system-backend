using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/quotationmaterial")]
public class QuotationMaterialController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public QuotationMaterialController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuotationMaterial>>> GetQuotationMaterial()
    {
        return await _context.QuotationMaterials.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<QuotationMaterial>> GetQuotationMaterial(int id)
    {
        var quotationMaterial = await _context.QuotationMaterials.FindAsync(id);
        if (quotationMaterial == null)
        {
            return NotFound();
        }
        return quotationMaterial;
    }

    [HttpPost]
public async Task<ActionResult> CreateQuotationMaterials([FromBody] List<QuotationMaterial> quotationMaterials)
{
    if (quotationMaterials == null || !quotationMaterials.Any())
    {
        return BadRequest(new { error = "No materials provided." });
    }

    _context.QuotationMaterials.AddRange(quotationMaterials);
    await _context.SaveChangesAsync();

    return Ok(new { message = "Materials saved successfully.", data = quotationMaterials });
}


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuotationMaterial(int id, [FromBody] QuotationMaterial quotationMaterial)
    {
        if (id != quotationMaterial.Id)
        {
            return BadRequest();
        }

        _context.Entry(quotationMaterial).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuotationMaterial(int id)
    {
        var quotationMaterial = await _context.QuotationMaterials.FindAsync(id);
        if (quotationMaterial == null)
        {
            return NotFound();
        }

        _context.QuotationMaterials.Remove(quotationMaterial);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}