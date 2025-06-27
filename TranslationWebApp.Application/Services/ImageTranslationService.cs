using TranslationWebApp.Application.Providers;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Application.Services;

/// <summary>
/// Dịch vụ chính xử lý dịch ảnh: OCR + Translate
/// </summary>
public class ImageTranslationService : IImageTranslationService
{
    private readonly IImageOcrProvider _ocrProvider;

    public ImageTranslationService(IImageOcrProvider ocrProvider)
    {
        _ocrProvider = ocrProvider;
    }

    /// <summary>
    /// Thực hiện trích text từ ảnh, sau đó dịch text
    /// </summary>
    public async Task<ImageTranslationResult> TranslateImageAsync(ImageTranslationRequest request)
    {
        if (request.ImageData == null || request.ImageData == Stream.Null)
            throw new ArgumentException("Không có ảnh hợp lệ.");

        // OCR: Trích text từ ảnh
        var extractedText = await _ocrProvider.ExtractTextFromImageAsync(request.ImageData);

        if (string.IsNullOrWhiteSpace(extractedText))
            throw new InvalidOperationException("Không trích được văn bản từ ảnh.");

        // TODO: gọi dịch văn bản bằng Azure Translator hoặc Google API
        var translatedText = await TranslateTextAsync(extractedText, request.FromLanguage, request.ToLanguage);

        return new ImageTranslationResult
        {
            ExtractedText = extractedText,
            TranslatedText = translatedText
        };
    }

    /// <summary>
    /// Tạm thời giả lập dịch văn bản (sẽ thay bằng Azure Translator thật)
    /// </summary>
    private Task<string> TranslateTextAsync(string text, string fromLang, string toLang)
    {
        // TODO: dùng Google hoặc Azure Translator provider thật
        return Task.FromResult($"[Giả lập dịch từ {fromLang} → {toLang}]: {text}");
    }
}
