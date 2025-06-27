using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Application.Services;

/// <summary>
/// Dịch vụ xử lý dịch tài liệu (chuẩn bị file, upload, gọi Azure, trả kết quả)
/// </summary>
public class DocumentTranslationService : IDocumentTranslationService
{
    // TODO: sau này sẽ inject provider gọi Azure thực sự (tầng Infrastructure)
    // Hiện tại tạm dùng bản giả lập để hoàn thiện luồng xử lý
    public async Task<DocumentTranslationResult> TranslateDocumentAsync(DocumentTranslationRequest request)
    {
        if (request.DocumentData == null || request.DocumentData == Stream.Null)
            throw new ArgumentException("Không có tài liệu hợp lệ để dịch.");

        // TODO: bước này sẽ thực hiện:
        // 1. Upload tài liệu vào Azure Blob (container nguồn)
        // 2. Gọi Azure Document Translation để xử lý
        // 3. Theo dõi trạng thái cho tới khi hoàn tất
        // 4. Trả lại URL tải về từ container đích

        // 🧪 Tạm thời trả kết quả giả lập
        await Task.Delay(1000); // giả lập thời gian xử lý

        return new DocumentTranslationResult
        {
            DownloadUrl = "https://yourdomain.com/downloads/translated-doc.pdf",
            Status = "Succeeded",
            ErrorMessage = null
        };
    }
}
