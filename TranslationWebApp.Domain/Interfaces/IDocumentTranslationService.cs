using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Domain.Interfaces;

/// <summary>
/// Interface dịch vụ dịch tài liệu (upload, xử lý, trả kết quả)
/// </summary>
public interface IDocumentTranslationService
{
    /// <summary>
    /// Thực hiện dịch tài liệu và trả kết quả
    /// </summary>
    /// <param name="request">Yêu cầu dịch tài liệu</param>
    /// <returns>Kết quả gồm trạng thái và URL tải về</returns>
    Task<DocumentTranslationResult> TranslateDocumentAsync(DocumentTranslationRequest request);
}
