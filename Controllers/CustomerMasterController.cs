using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{[ApiController]
[Route("api/customer")]
public class CustomerMasterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerMasterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerMaster>>> GetCustomerMasters()
    {
        return await _context.CustomerMasters.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerMaster>> GetCustomerMaster(int id)
    {
        var customer = await _context.CustomerMasters.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return customer;
    }

    [HttpPost]
    public async Task<ActionResult<CustomerMaster>> CreateCustomerMaster([FromBody] CustomerMaster customer)
    {
        _context.CustomerMasters.Add(customer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCustomerMaster), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomerMaster(int id, [FromBody] CustomerMaster customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomerMaster(int id)
    {
        var customer = await _context.CustomerMasters.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.CustomerMasters.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
}