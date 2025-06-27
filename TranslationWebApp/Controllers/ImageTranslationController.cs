using Microsoft.AspNetCore.Mvc;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Controllers;

/// <summary>
/// Controller xử lý yêu cầu dịch ảnh (OCR + Translate)
/// </summary>
[ApiController]
[Route("api/image")]
public class ImageTranslationController : ControllerBase
{
    private readonly IImageTranslationService _imageTranslationService;

    public ImageTranslationController(IImageTranslationService imageTranslationService)
    {
        _imageTranslationService = imageTranslationService;
    }

    /// <summary>
    /// API nhận ảnh và trả kết quả OCR + dịch
    /// </summary>
    /// <param name="imageFile">Tệp ảnh upload (jpg/png/webp...)</param>
    /// <param name="fromLanguage">Ngôn ngữ gốc (ví dụ: "en")</param>
    /// <param name="toLanguage">Ngôn ngữ đích (ví dụ: "vi")</param>
    /// <returns>Kết quả gồm văn bản trích xuất và bản dịch</returns>
    [HttpPost("translate")]
    public async Task<IActionResult> TranslateImage(
        IFormFile imageFile,
        [FromForm] string fromLanguage = "en",
        [FromForm] string toLanguage = "vi")
    {
        if (imageFile == null || imageFile.Length == 0)
            return BadRequest("Không có ảnh được gửi lên.");

        // Đọc ảnh vào stream
        using var stream = imageFile.OpenReadStream();

        // Tạo request model
        var request = new ImageTranslationRequest
        {
            ImageData = stream,
            FromLanguage = fromLanguage,
            ToLanguage = toLanguage
        };

        try
        {
            var result = await _imageTranslationService.TranslateImageAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Lỗi xử lý ảnh: {ex.Message}");
        }
    }
}
