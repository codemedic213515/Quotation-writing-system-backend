using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/material")]
public class MaterialMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MaterialMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MaterialMaster>>> GetMaterialMasters(
        [FromQuery] int category1,
        [FromQuery] int category2,
        [FromQuery] int category3
       )
    {
        if(category1 <=0 ||category2<=0|| category3<=0){
            return BadRequest("Invalid category1 or category2 or category3 or category 4 values.");
        }
        var filterMaterial = await _context.MaterialMasters
        .Where(m=> m.Category1==category1 && m.Category2==category2 && m.Category3==category3)
        .ToListAsync();
        if(!filterMaterial.Any()){
            return NotFound("No material masters found");
        }
        return Ok(filterMaterial);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialMaster>> GetMaterialMaster(int id)
    {
        var material = await _context.MaterialMasters.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return material;
    }

    [HttpPost]
    public async Task<ActionResult<MaterialMaster>> CreateMaterialMaster([FromBody] MaterialMaster material)
    {
        _context.MaterialMasters.Add(material);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMaterialMaster), new { id = material.Id }, material);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaterialMaster(int id, [FromBody] MaterialMaster material)
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
    public async Task<IActionResult> DeleteMaterialMaster(int id)
    {
        var material = await _context.MaterialMasters.FindAsync(id);
        if (material == null)
        {
            return NotFound();
        }

        _context.MaterialMasters.Remove(material);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}