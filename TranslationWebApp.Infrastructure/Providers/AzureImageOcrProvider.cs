using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Options;
using TranslationWebApp.Application.Providers;
using TranslationWebApp.Infrastructure.Configuration;

namespace TranslationWebApp.Infrastructure.Providers;

/// <summary>
/// Provider OCR xử lý ảnh bằng Azure Computer Vision API (SDK cũ)
/// </summary>
public class AzureImageOcrProvider : IImageOcrProvider
{
    private readonly AzureOcrConfig _config;

    public AzureImageOcrProvider(IOptions<AzureOcrConfig> config)
    {
        _config = config.Value;
    }

    /// <summary>
    /// Thực hiện OCR từ ảnh, trả về văn bản trích được
    /// </summary>
    public async Task<string> ExtractTextFromImageAsync(Stream imageStream)
    {
        // 1. Tạo client sử dụng key + endpoint
        var credentials = new ApiKeyServiceClientCredentials(_config.SubscriptionKey);
        var client = new ComputerVisionClient(credentials)
        {
            Endpoint = _config.Endpoint
        };

        // 2. Gọi Read API (ReadInStreamAsync)
        var readOp = await client.ReadInStreamAsync(imageStream);

        // 3. Lấy Operation ID từ response header
        string operationId = readOp.OperationLocation.Split('/').Last();

        // 4. Poll kết quả đến khi xong
        ReadOperationResult result;
        do
        {
            await Task.Delay(1000); // Đợi 1 giây
            result = await client.GetReadResultAsync(Guid.Parse(operationId));
        }
        while (result.Status == OperationStatusCodes.Running || result.Status == OperationStatusCodes.NotStarted);

        if (result.Status != OperationStatusCodes.Succeeded)
            throw new InvalidOperationException("Không trích xuất được văn bản từ ảnh.");

        // 5. Trích tất cả dòng văn bản
        var lines = result.AnalyzeResult.ReadResults
            .SelectMany(page => page.Lines)
            .Select(line => line.Text);

        return string.Join(" ", lines);
    }
}
