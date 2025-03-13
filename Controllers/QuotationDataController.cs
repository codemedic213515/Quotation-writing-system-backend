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
    public async Task<IActionResult> GetFormattedQuotationQuotate([FromQuery] string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return BadRequest(new { Message = "Quotation number is required." });
        }

        var result = await _quotationService.GetFormattedQuotationQuotate(quotationNumber);

        if (result == null || !result.Any())
        {
            return NotFound(new { Message = "No data" });
        }

        return Ok(result);
    }

    [HttpGet("net")]
    public async Task<IActionResult> GetFormattedQuotationNet([FromQuery] string quotationNumber)
    {
        if (string.IsNullOrEmpty(quotationNumber))
        {
            return BadRequest(new { Message = "Quotation number is required." });
        }

        var result = await _quotationService.GetFormattedQuotationNet(quotationNumber);

        if (result == null || !result.Any())
        {
            return NotFound(new { Message = "No data" });
        }

        return Ok(result);
    }


}
