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
    private readonly ILogger<CustomerMasterController> _logger;

    public CustomerMasterController(ApplicationDbContext context, ILogger<CustomerMasterController> logger)
        {
            _context = context;
            _logger = logger;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerMaster>>> GetCustomerMasters()
    {
        return await _context.CustomerMasters.ToListAsync();
    }
[HttpGet("group")]
public async Task<ActionResult<IEnumerable<CustomerMaster>>> GetCustomerMasterByGroup([FromQuery] int number)
{
    try
    {
        var customers = await _context.CustomerMasters
            .Where(a => a.Group == number.ToString()) // Convert number to string
            .ToListAsync();

        return Ok(customers);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching data for Customer: {Customer}", number);
        return StatusCode(500, "Internal Server Error. Please try again later.");
    }
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
    // Check for duplicates based on Phone, Name, Email, Group, Fax, Hp, and Address
    var existingCustomer = await _context.CustomerMasters
        .FirstOrDefaultAsync(c => 
            c.Phone == customer.Phone || 
            c.Name == customer.Name || 
            c.Email == customer.Email || 
            c.Group == customer.Group || 
            c.Fax == customer.Fax || 
            c.Hp == customer.Hp || 
            c.Address == customer.Address);

    if (existingCustomer != null)
    {
        // Return 400 Bad Request if a duplicate is found with an appropriate error message
        return BadRequest("A customer with the same Phone, Name, Email, Group, Fax, Hp, or Address already exists.");
    }

    // Add the new customer if no duplicates are found
    _context.CustomerMasters.Add(customer);
    await _context.SaveChangesAsync();

    // Return 200 OK with the created customer object
    return Ok(customer);
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