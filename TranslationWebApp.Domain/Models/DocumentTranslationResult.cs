namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Kết quả dịch tài liệu
/// </summary>
public class DocumentTranslationResult
{
    /// <summary>
    /// Đường dẫn tải về file đã dịch (hoặc stream file)
    /// </summary>
    public string DownloadUrl { get; set; } = string.Empty;

    /// <summary>
    /// Trạng thái dịch (thành công/thất bại)
    /// </summary>
    public string Status { get; set; } = "Succeeded";

    /// <summary>
    /// Thông báo lỗi (nếu có)
    /// </summary>
    public string? ErrorMessage { get; set; }
}
