using Microsoft.AspNetCore.Mvc;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Controllers;

/// <summary>
/// Controller xử lý API dịch văn bản
/// </summary>
[ApiController]
[Route("/api/text/translate")]
public class TranslationController : ControllerBase
{
    private readonly ITextTranslationService _translationService;

    /// <summary>
    /// Inject service nghiệp vụ dịch văn bản
    /// </summary>
    public TranslationController(ITextTranslationService translationService)
    {
        _translationService = translationService;
    }

    /// <summary>
    /// API dịch văn bản
    /// </summary>
    /// <param name="request">Yêu cầu dịch (text, sourceLang, targetLang)</param>
    /// <returns>Kết quả bản dịch</returns>
    [HttpPost]
    public async Task<ActionResult<TranslationResult>> Translate([FromBody] TranslationRequest request)
    {
        try
        {
            // Gọi dịch
            var result = await _translationService.TranslateAsync(request);

            // Trả về kết quả thành công
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            // Trường hợp lỗi input: trả về 400
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            // Lỗi không xác định: trả về 500
            return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
        }
    }
}
