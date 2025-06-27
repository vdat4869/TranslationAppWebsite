using TranslationWebApp.Application.Providers;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Application.Services;

/// <summary>
/// Dịch vụ xử lý nghiệp vụ dịch giọng nói, gọi đến provider thực tế (Azure)
/// </summary>
public class SpeechTranslationService : ISpeechTranslationService
{
    private readonly IAudioTranslationProvider _provider;

    /// <summary>
    /// Inject provider xử lý giọng nói
    /// </summary>
    public SpeechTranslationService(IAudioTranslationProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Thực hiện dịch giọng nói: speech → text → translate → audio
    /// </summary>
    /// <param name="request">Dữ liệu giọng nói và ngôn ngữ</param>
    /// <returns>Kết quả gồm text gốc, text dịch và file âm thanh</returns>
    public async Task<SpeechTranslationResult> TranslateSpeechAsync(SpeechTranslationRequest request)
    {
        // Kiểm tra đầu vào
        if (request.AudioData == null || request.AudioData == Stream.Null)
            throw new ArgumentException("Không có dữ liệu âm thanh.");

        // Gọi provider thực hiện xử lý
        return await _provider.TranslateFromSpeechAsync(request);
    }
}
