using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Application.Providers;

/// <summary>
/// Giao diện provider dùng để xử lý dịch giọng nói (speech → text → translate → speech)
/// </summary>
public interface IAudioTranslationProvider
{
    /// <summary>
    /// Dịch giọng nói và trả về kết quả (text & audio)
    /// </summary>
    Task<SpeechTranslationResult> TranslateFromSpeechAsync(SpeechTranslationRequest request);
}
