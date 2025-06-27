namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Kết quả dịch ảnh: gồm văn bản gốc và bản dịch
/// </summary>
public class ImageTranslationResult
{
    /// <summary>
    /// Văn bản đã trích xuất từ ảnh
    /// </summary>
    public string ExtractedText { get; set; } = string.Empty;

    /// <summary>
    /// Văn bản đã được dịch sang ngôn ngữ đích
    /// </summary>
    public string TranslatedText { get; set; } = string.Empty;
}
