using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/quotationdata")]
[ApiController]
public class QuotationDataController : ControllerBase
{
    private readonly QuotationService _quotationService;

    public QuotationDataController(QuotationService quotationService)
    {
        _quotationService = quotationService;
    }

    [HttpGet("formatted")]
    public async Task<IActionResult> GetFormattedQuotationData([FromQuery] string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return BadRequest(new { Message = "Quotation number is required." });
        }

        var result = await _quotationService.GetFormattedQuotationData(quotationNumber);

        if (result == null || !result.Any())
        {
            return NotFound(new { Message = "No data" });
        }

        return Ok(result);
    }
}
