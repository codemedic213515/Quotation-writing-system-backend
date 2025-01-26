using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
[ApiController]
[Route("api/category2")]
public class CategoryMaterialMaster2Controller : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategoryMaterialMaster2Controller> _logger;
    public CategoryMaterialMaster2Controller(ApplicationDbContext context, ILogger<CategoryMaterialMaster2Controller> logger)
    {
        _context = context;
        _logger = logger;
    }

   [HttpGet]
public async Task<ActionResult<IEnumerable<CategoryMaterialMaster2>>> GetCategoryMaterialMasters([FromQuery] int category1)
{

    var filteredMaterials = await _context.CategoryMaterialMasters2
        .Where(m => m.Category1 == category1)
        .ToListAsync();

    if (!filteredMaterials.Any())
    {
        return NotFound("No materials found for the given category1.");
    }

    return Ok(filteredMaterials);
}


    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryMaterialMaster2>> GetCategoryMaterialMaster(int id)
    {
        var material = await _context.CategoryMaterialMasters2.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return material;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryMaterialMaster2>> CreateCategoryMaterialMaster([FromBody] CategoryMaterialMaster2 material)
    {
        _context.CategoryMaterialMasters2.Add(material);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategoryMaterialMaster), new { id = material.Id }, material);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryMaterialMaster(int id, [FromBody] CategoryMaterialMaster2 material)
    {
        if (id != material.Id)
        {
            return BadRequest();
        }

        _context.Entry(material).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryMaterialMaster(int id)
    {
        var material = await _context.CategoryMaterialMasters2.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }

        _context.CategoryMaterialMasters2.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}


}