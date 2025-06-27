using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Domain.Interfaces;

/// <summary>
/// Interface nghiệp vụ chính: dịch giọng nói (speech → text → translate → speech)
/// </summary>
public interface ISpeechTranslationService
{
    /// <summary>
    /// Thực hiện toàn bộ quá trình dịch giọng nói
    /// </summary>
    /// <param name="request">Yêu cầu đầu vào (file audio, from, to)</param>
    /// <returns>Kết quả chứa text và âm thanh đã dịch</returns>
    Task<SpeechTranslationResult> TranslateSpeechAsync(SpeechTranslationRequest request);
}
