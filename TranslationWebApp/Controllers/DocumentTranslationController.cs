using Microsoft.AspNetCore.Mvc;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Controllers;

/// <summary>
/// Controller xử lý yêu cầu dịch tài liệu (PDF, Word, PowerPoint...)
/// </summary>
[ApiController]
[Route("api/document")]
public class DocumentTranslationController : ControllerBase
{
    private readonly IDocumentTranslationService _documentTranslationService;

    public DocumentTranslationController(IDocumentTranslationService documentTranslationService)
    {
        _documentTranslationService = documentTranslationService;
    }

    /// <summary>
    /// API upload file và dịch sang ngôn ngữ đích
    /// </summary>
    /// <param name="file">Tệp tài liệu cần dịch</param>
    /// <param name="fromLanguage">Ngôn ngữ nguồn (vd: en)</param>
    /// <param name="toLanguage">Ngôn ngữ đích (vd: vi)</param>
    [HttpPost("translate")]
    public async Task<IActionResult> TranslateDocument(
        IFormFile file,
        [FromForm] string fromLanguage = "en",
        [FromForm] string toLanguage = "vi")
    {
        if (file == null || file.Length == 0)
            return BadRequest("Không có tệp tài liệu được gửi lên.");

        using var stream = file.OpenReadStream();

        var request = new DocumentTranslationRequest
        {
            DocumentData = stream,
            FileName = file.FileName,
            FromLanguage = fromLanguage,
            ToLanguage = toLanguage
        };

        try
        {
            var result = await _documentTranslationService.TranslateDocumentAsync(request);
            if (result.Status == "Succeeded")
            {
                return Ok(result);
            }

            return StatusCode(500, result.ErrorMessage ?? "Dịch tài liệu thất bại.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi xử lý: {ex.Message}");
        }
    }
}
