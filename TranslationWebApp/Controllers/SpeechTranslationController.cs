using Microsoft.AspNetCore.Mvc;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Controllers;

/// <summary>
/// API xử lý dịch giọng nói
/// </summary>
[ApiController]
[Route("api/speech")]
public class SpeechTranslationController : ControllerBase
{
    private readonly ISpeechTranslationService _speechService;

    public SpeechTranslationController(ISpeechTranslationService speechService)
    {
        _speechService = speechService;
    }

    /// <summary>
    /// API: Nhận file âm thanh, trả về bản dịch và audio đã dịch
    /// </summary>
    /// <param name="audioFile">file giọng nói (mp3, wav...)</param>
    /// <param name="fromLanguage">ngôn ngữ đầu vào (vd: en-US)</param>
    /// <param name="toLanguage">ngôn ngữ muốn dịch sang (vd: vi)</param>
    [HttpPost("translate")]
    [RequestSizeLimit(10_000_000)] // Giới hạn 10MB
    public async Task<IActionResult> TranslateSpeechAsync([FromForm] SpeechTranslationRequest request)
    {
        if (request.AudioFile == null || request.AudioFile.Length == 0)
            return BadRequest("Không có file âm thanh được gửi lên.");

        using var stream = request.AudioFile.OpenReadStream();

        var internalRequest = new Domain.Models.SpeechTranslationRequest
        {
            AudioData = stream,
            FromLanguage = request.FromLanguage,
            ToLanguage = request.ToLanguage
        };

        try
        {
            var result = await _speechService.TranslateSpeechAsync(internalRequest);

            return Ok(new
            {
                recognized = result.RecognizedText,
                translated = result.TranslatedText,
                audioBase64 = result.TranslatedAudio != null
                    ? Convert.ToBase64String(result.TranslatedAudio)
                    : null
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Lỗi xử lý giọng nói", detail = ex.Message });
        }
    }

}
