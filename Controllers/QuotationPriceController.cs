using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/quotationprice")]
[ApiController]
public class QuotationController : ControllerBase
{
    private readonly QuotationService _quotationService;

    public QuotationController(QuotationService quotationService)
    {
        _quotationService = quotationService;
    }

    [HttpGet("calculate")]
    public async Task<IActionResult> GetQuotationCost([FromQuery] string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return BadRequest(new { Message = "Quotation number is required." });
        }

        var result = await _quotationService.CalculateQuotationCost(quotationNumber);

        if (result == null)
        {
            return NotFound(new { Message = "No data found for the given quotation number." });
        }

        return Ok(result);
    }
}
