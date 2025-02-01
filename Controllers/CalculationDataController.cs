using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
    [Route("api/calculationdata")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalculationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/calculationdata/{number}
        [HttpGet("{number}")]
        public async Task<ActionResult<CalculationData>> GetCalculationData(string number)
        {
            var calculationData = await _context.CalculationDatas
                                                .FirstOrDefaultAsync(c => c.Number == number);

            if (calculationData == null)
            {
                return NotFound();
            }

            return calculationData;
        }

        // PUT: api/calculationdata/{number}
[HttpPut("{number}")]
public async Task<IActionResult> UpdateCalculationData(string number, CalculationData calculationData)
{
    // Check if the number matches the incoming data
    if (number != calculationData.Number)
    {
        return BadRequest("The provided number does not match the data number.");
    }

    // Try to find the existing record by number
    var existingData = await _context.CalculationDatas
                                      .FirstOrDefaultAsync(c => c.Number == number);

    if (existingData == null)
    {
        // If no data is found, add the new data
        _context.CalculationDatas.Add(calculationData);
    }
    else
    {
        // If the data exists, update it
        _context.Entry(existingData).CurrentValues.SetValues(calculationData);
    }

    try
    {
        // Save changes (whether it's an update or insert)
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        // Handle concurrency issues
        if (!CalculationDataExists(number))
        {
            return NotFound();
        }
        else
        {
            throw;
        }
    }

    // Return NoContent response if successful
    return Ok(existingData);
}


        // POST: api/calculationdata
        [HttpPost]
        public async Task<ActionResult<CalculationData>> CreateCalculationData(CalculationData calculationData)
        {
            _context.CalculationDatas.Add(calculationData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCalculationData), new { number = calculationData.Number }, calculationData);
        }

        private bool CalculationDataExists(string number)
        {
            return _context.CalculationDatas.Any(e => e.Number == number);
        }
    }
}
