using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
    [ApiController]
[Route("api/category4")]
public class CategoryMaterialMaster4Controller : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoryMaterialMaster4Controller(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryMaterialMaster4>>> GetCategoryMaterialMasters()
    {
        return await _context.CategoryMaterialMasters4.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryMaterialMaster4>> GetCategoryMaterialMaster(int id)
    {
        var material = await _context.CategoryMaterialMasters4.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return material;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryMaterialMaster4>> CreateCategoryMaterialMaster([FromBody] CategoryMaterialMaster4 material)
    {
        _context.CategoryMaterialMasters4.Add(material);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategoryMaterialMaster), new { id = material.Id }, material);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoryMaterialMaster(int id, [FromBody] CategoryMaterialMaster4 material)
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
        var material = await _context.CategoryMaterialMasters4.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }

        _context.CategoryMaterialMasters4.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

}