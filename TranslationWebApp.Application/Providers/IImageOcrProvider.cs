namespace TranslationWebApp.Application.Providers;

/// <summary>
/// Giao diện provider xử lý OCR ảnh (trích text từ ảnh)
/// </summary>
public interface IImageOcrProvider
{
    /// <summary>
    /// Trích xuất văn bản từ ảnh (OCR)
    /// </summary>
    /// <param name="imageStream">Stream ảnh (jpg/png)</param>
    /// <returns>Chuỗi văn bản trích được</returns>
    Task<string> ExtractTextFromImageAsync(Stream imageStream);
}
