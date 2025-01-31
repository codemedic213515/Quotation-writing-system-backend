using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationWritingSystem.Data;
using QuotationWritingSystem.Models;
using System.Threading.Tasks;

namespace QuotationWritingSystem.Controllers
{
    [Route("api/quotationdata")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalculationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Calculation
        [HttpGet("{id}")]
        public async Task<ActionResult<CalculationData>> GetCalculationData(int id)
        {
            var calculationData = await _context.CalculationDatas.FindAsync(id);

            if (calculationData == null)
            {
                return NotFound();
            }

            return calculationData;
        }

        // PUT: api/Calculation/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalculationData(int id, CalculationData calculationData)
        {
            if (id != calculationData.Id)
            {
                return BadRequest();
            }

            _context.Entry(calculationData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculationDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Calculation
        [HttpPost]
        public async Task<ActionResult<CalculationData>> CreateCalculationData(CalculationData calculationData)
        {
            _context.CalculationDatas.Add(calculationData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCalculationData), new { id = calculationData.Id }, calculationData);
        }

        private bool CalculationDataExists(int id)
        {
            return _context.CalculationDatas.Any(e => e.Id == id);
        }
    }
}
