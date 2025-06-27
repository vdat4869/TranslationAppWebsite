using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;
using TranslationWebApp.Application.Providers;

namespace TranslationWebApp.Application.Services;

/// <summary>
/// Dịch văn bản bằng cách sử dụng provider (Google hoặc Azure) tuỳ cấu hình
/// </summary>
public class TextTranslationService : ITextTranslationService
{
    private readonly IEnumerable<ITranslationProvider> _providers;

    /// <summary>
    /// Inject danh sách các provider có thể dùng (Google, Azure)
    /// </summary>
    /// <param name="providers">Danh sách các provider đã đăng ký</param>
    public TextTranslationService(IEnumerable<ITranslationProvider> providers)
    {
        _providers = providers;
    }

    /// <summary>
    /// Hàm xử lý dịch văn bản: nhận yêu cầu, chọn provider và gọi dịch
    /// </summary>
    /// <param name="request">Yêu cầu dịch (text, sourceLang, targetLang)</param>
    /// <returns>Kết quả bản dịch (text đã dịch, ngôn ngữ detect)</returns>
    public async Task<TranslationResult> TranslateAsync(TranslationRequest request)
    {
        // Kiểm tra đầu vào hợp lệ
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentException("Văn bản không được để trống.");

        // Lấy provider đầu tiên (ở giai đoạn đầu ta chỉ đăng ký 1 provider, sau này có thể chọn dựa theo config)
        var provider = _providers.FirstOrDefault();
        if (provider == null)
            throw new InvalidOperationException("Không có dịch vụ dịch nào khả dụng.");

        // Gọi hàm dịch thực tế từ provider
        return await provider.TranslateAsync(request);
    }
}
