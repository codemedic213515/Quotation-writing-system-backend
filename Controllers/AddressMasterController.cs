using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressMasterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AddressMasterController> _logger;

        public AddressMasterController(ApplicationDbContext context, ILogger<AddressMasterController> logger)
        {
            _context = context;
            _logger = logger;
        }

       [HttpGet]
public async Task<ActionResult<IEnumerable<AddressMaster>>> GetAddresses([FromQuery] string city)
{
var a = "%" + city + "%";
_logger.LogInformation("aaa:{a}", a);
    try
    {
        // Validate the city parameter
        if (string.IsNullOrEmpty(city))
        {
            return BadRequest("Prefecture parameter is required.");
        }

        // SQL query using parameterized LIKE with `FromSqlRaw`
        var query = "SELECT * FROM AddressMaster WHERE Prefecture LIKE {0}";
_logger.LogInformation("aaa:{query}", query);
        
        // Execute raw SQL query
        var addresses = await _context.AddressMaster
            .FromSqlRaw(query, a )  // Parameterized query for LIKE
            .ToListAsync();  // Execute and fetch results

        // Return the filtered addresses
        return Ok(addresses);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching data for Prefecture: {Prefecture}", city);
        return StatusCode(500, "Internal Server Error. Please try again later.");
    }
}


    [HttpPost]
    public async Task<ActionResult<AddressMaster>> CreateAddress([FromBody] AddressMaster address)
    {
        _context.AddressMaster.Add(address);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAddresses), new { id = address.Id }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressMaster address)
    {
        if (id != address.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var existingAddress = await _context.AddressMaster.FindAsync(id);
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
        var address = await _context.AddressMaster.FindAsync(id);
        if (address == null)
        {
            return NotFound("Address not found.");
        }

        _context.AddressMaster.Remove(address);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}