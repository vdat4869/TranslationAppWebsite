using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Application.Providers;

/// <summary>
/// Giao diện chuẩn cho mọi dịch vụ dịch (Google, Azure)
/// </summary>
public interface ITranslationProvider
{
    /// <summary>
    /// Dịch văn bản và trả về kết quả
    /// </summary>
    Task<TranslationResult> TranslateAsync(TranslationRequest request);
}
