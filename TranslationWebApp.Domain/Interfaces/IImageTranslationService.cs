using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Domain.Interfaces;

/// <summary>
/// Interface dịch vụ dịch ảnh (trích văn bản từ ảnh và dịch nó)
/// </summary>
public interface IImageTranslationService
{
    /// <summary>
    /// Thực hiện dịch ảnh thành văn bản và bản dịch
    /// </summary>
    /// <param name="request">Yêu cầu dịch ảnh</param>
    /// <returns>Kết quả chứa text trích xuất và text đã dịch</returns>
    Task<ImageTranslationResult> TranslateImageAsync(ImageTranslationRequest request);
}
