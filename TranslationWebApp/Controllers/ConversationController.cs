using Microsoft.AspNetCore.Mvc;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Controllers;

/// <summary>
/// API xử lý hội thoại song ngữ: nhận giọng nói, dịch và trả về kết quả
/// </summary>
[ApiController]
[Route("api/conversation")]
public class ConversationController : ControllerBase
{
    private readonly IConversationService _conversationService;

    public ConversationController(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    /// <summary>
    /// API: Nhận giọng nói, dịch và trả lại kết quả (text + giọng nói dịch)
    /// </summary>
    /// <param name="audioFile">File âm thanh (mp3, wav...)</param>
    /// <param name="fromLanguage">Ngôn ngữ đầu vào (vd: en-US)</param>
    /// <param name="toLanguage">Ngôn ngữ đích (vd: vi)</param>
    /// <param name="speaker">Người nói (tùy chọn)</param>
    [HttpPost("translate")]
    [RequestSizeLimit(10_000_000)] // 10MB
    public async Task<IActionResult> TranslateAsync(
        [FromForm] IFormFile audioFile,
        [FromForm] string fromLanguage = "en-US",
        [FromForm] string toLanguage = "vi",
        [FromForm] string speaker = "User")
    {
        if (audioFile == null || audioFile.Length == 0)
            return BadRequest("Không có file âm thanh được gửi lên.");

        var request = new ConversationRequest
        {
            AudioFile = audioFile,
            FromLanguage = fromLanguage,
            ToLanguage = toLanguage,
            Speaker = speaker
        };

        var result = await _conversationService.HandleConversationAsync(request);

        return Ok(new
        {
            speaker = result.Speaker,
            recognized = result.RecognizedText,
            translated = result.TranslatedText,
            audioBase64 = result.TranslatedAudio != null
                ? Convert.ToBase64String(result.TranslatedAudio)
                : null
        });
    }
}
