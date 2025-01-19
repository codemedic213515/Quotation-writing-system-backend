using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
   [ApiController]
[Route("api/address")]
public class AddressMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AddressMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressMaster>>> GetAddresses()
    {
        return await _context.AddressMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressMaster>> GetAddress(int id)
    {
        var address = await _context.AddressMasters.FindAsync(id);
        if (address == null)
        {
            return NotFound("Address not found.");
        }
        return Ok(address);
    }

    [HttpPost]
    public async Task<ActionResult<AddressMaster>> CreateAddress([FromBody] AddressMaster address)
    {
        _context.AddressMasters.Add(address);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressMaster address)
    {
        if (id != address.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var existingAddress = await _context.AddressMasters.FindAsync(id);
        if (existingAddress == null)
        {
            return NotFound("Address not found.");
        }

        existingAddress.ZipCode = address.ZipCode;
        existingAddress.Prefecture = address.Prefecture;
        existingAddress.City = address.City;
        existingAddress.Street = address.Street;
        existingAddress.Delete = address.Delete;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var address = await _context.AddressMasters.FindAsync(id);
        if (address == null)
        {
            return NotFound("Address not found.");
        }

        _context.AddressMasters.Remove(address);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}