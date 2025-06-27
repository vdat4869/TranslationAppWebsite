namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Yêu cầu dịch ảnh: gồm ảnh và thông tin ngôn ngữ
/// </summary>
public class ImageTranslationRequest
{
    /// <summary>
    /// Dữ liệu ảnh upload từ client (jpg, png, webp...) dưới dạng stream
    /// </summary>
    public Stream ImageData { get; set; } = Stream.Null;

    /// <summary>
    /// Ngôn ngữ của văn bản trong ảnh (ví dụ: "en", "ja")
    /// </summary>
    public string FromLanguage { get; set; } = "en";

    /// <summary>
    /// Ngôn ngữ đích muốn dịch sang (ví dụ: "vi", "fr")
    /// </summary>
    public string ToLanguage { get; set; } = "vi";
}
