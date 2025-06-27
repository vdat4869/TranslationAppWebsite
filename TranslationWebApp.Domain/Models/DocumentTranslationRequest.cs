namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Yêu cầu dịch tài liệu (upload file + thông tin ngôn ngữ)
/// </summary>
public class DocumentTranslationRequest
{
    /// <summary>
    /// Tệp tài liệu cần dịch (PDF, DOCX, PPTX, XLSX...)
    /// </summary>
    public Stream DocumentData { get; set; } = Stream.Null;

    /// <summary>
    /// Tên gốc của tài liệu (dùng để lấy phần mở rộng file)
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Ngôn ngữ nguồn (ví dụ: "en")
    /// </summary>
    public string FromLanguage { get; set; } = "en";

    /// <summary>
    /// Ngôn ngữ đích muốn dịch sang (ví dụ: "vi")
    /// </summary>
    public string ToLanguage { get; set; } = "vi";
}
