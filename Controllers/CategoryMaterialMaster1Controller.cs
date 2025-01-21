using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{


[ApiController]
[Route("api/category1")]
public class CategoryMaterialMaster1Controller : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryMaterialMaster1Controller(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryMaterialMaster1>>> GetCategoryMaterialMasters()
    {
        return await _context.CategoryMaterialMasters1.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryMaterialMaster1>> GetCategoryMaterialMaster(int id)
    {
        var material = await _context.CategoryMaterialMasters1.FindAsync(id);
        if (material == null)
        {
            return NotFound("Material not found.");
        }
        return Ok(material);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryMaterialMaster1>> CreateCategoryMaterialMaster([FromBody] CategoryMaterialMaster1 material)
    {
        _context.CategoryMaterialMasters1.Add(material);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategoryMaterialMaster), new { id = material.Id }, material);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryMaterialMaster(int id, [FromBody] CategoryMaterialMaster1 material)
    {
        if (id != material.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var existingMaterial = await _context.CategoryMaterialMasters1.FindAsync(id);
        if (existingMaterial == null)
        {
            return NotFound("Material not found.");
        }

        existingMaterial.Name = material.Name;

        existingMaterial.Delete = material.Delete;


        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoryMaterialMaster(int id)
    {
        var material = await _context.CategoryMaterialMasters1.FindAsync(id);
        if (material == null)
        {
            return NotFound("Material not found.");
        }

        _context.CategoryMaterialMasters1.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}