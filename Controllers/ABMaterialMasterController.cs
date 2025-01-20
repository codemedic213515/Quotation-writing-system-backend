using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;

namespace QuotationWritingSystem.Controllers
{
    [ApiController]
    [Route("api/abmaterial")]
    public class ABMaterialMasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ABMaterialMasterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ABMaterialMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ABMaterialMaster>>> GetABMaterialMaster()
        {
            return await _context.ABMaterialMaster.ToListAsync();
        }

        // GET: api/ABMaterialMaster/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ABMaterialMaster>> GetABMaterialMaster(int id)
        {
            var material = await _context.ABMaterialMaster.FindAsync(id);
            if (material == null)
            {
                return NotFound("Material not found.");
            }
            return Ok(material);
        }

        // POST: api/ABMaterialMaster
        [HttpPost]
        public async Task<ActionResult<ABMaterialMaster>> CreateABMaterialMaster([FromBody] ABMaterialMaster material)
        {
            _context.ABMaterialMaster.Add(material);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetABMaterialMaster), new { id = material.Id }, material);
        }

        // PUT: api/ABMaterialMaster/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateABMaterialMaster(int id, [FromBody] ABMaterialMaster material)
        {
            if (id != material.Id)
            {
                return BadRequest("ID mismatch.");
            }

            var existingMaterial = await _context.ABMaterialMaster.FindAsync(id);
            if (existingMaterial == null)
            {
                return NotFound("Material not found.");
            }

            existingMaterial.Name = material.Name;
            existingMaterial.ABCode = material.ABCode;
            existingMaterial.Rate = material.Rate;
            existingMaterial.Cost = material.Cost;
            existingMaterial.CategoryName = material.CategoryName;
            existingMaterial.OtherRate = material.OtherRate;
            existingMaterial.Delete = material.Delete;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ABMaterialMaster/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteABMaterialMaster(int id)
        {
            var material = await _context.ABMaterialMaster.FindAsync(id);
            if (material == null)
            {
                return NotFound("Material not found.");
            }

            _context.ABMaterialMaster.Remove(material);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
