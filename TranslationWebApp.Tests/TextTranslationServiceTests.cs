using System.Threading.Tasks;
using Moq;
using Xunit;
using TranslationWebApp.Application.Services;
using TranslationWebApp.Application.Providers;
using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Tests;

/// <summary>
/// Unit test cho lớp TextTranslationService (dịch văn bản)
/// </summary>
public class TextTranslationServiceTests
{
    [Fact]
    public async Task TranslateAsync_ValidInput_ShouldReturnTranslatedResult()
    {
        // Arrange: tạo request đầu vào
        var request = new TranslationRequest
        {
            Text = "Hello",
            SourceLanguage = "en",
            TargetLanguage = "vi"
        };

        // Tạo kết quả mong đợi
        var expectedResult = new TranslationResult
        {
            TranslatedText = "Xin chào",
            DetectedSourceLanguage = "en"
        };

        // Mock provider (giả lập hành vi)
        var mockProvider = new Mock<ITranslationProvider>();
        mockProvider
            .Setup(p => p.TranslateAsync(It.IsAny<TranslationRequest>()))
            .ReturnsAsync(expectedResult);

        // Inject mock vào service
        var service = new TextTranslationService(new[] { mockProvider.Object });

        // Act: gọi hàm dịch
        var result = await service.TranslateAsync(request);

        // Assert: kiểm tra kết quả đúng như mong đợi
        Assert.Equal(expectedResult.TranslatedText, result.TranslatedText);
        Assert.Equal(expectedResult.DetectedSourceLanguage, result.DetectedSourceLanguage);
    }

    [Fact]
    public async Task TranslateAsync_EmptyText_ShouldThrowArgumentException()
    {
        // Arrange: tạo request trống
        var request = new TranslationRequest
        {
            Text = "", // không có nội dung
            SourceLanguage = "en",
            TargetLanguage = "vi"
        };

        var mockProvider = new Mock<ITranslationProvider>();
        var service = new TextTranslationService(new[] { mockProvider.Object });

        // Act + Assert: gọi hàm và kiểm tra ném lỗi đúng
        await Assert.ThrowsAsync<ArgumentException>(() => service.TranslateAsync(request));
    }

    [Fact]
    public async Task TranslateAsync_NoProvider_ShouldThrowInvalidOperationException()
    {
        // Arrange: không inject provider nào cả
        var service = new TextTranslationService(new List<ITranslationProvider>());

        var request = new TranslationRequest
        {
            Text = "Test",
            SourceLanguage = "en",
            TargetLanguage = "vi"
        };

        // Act + Assert: gọi hàm và kiểm tra ném lỗi
        await Assert.ThrowsAsync<InvalidOperationException>(() => service.TranslateAsync(request));
    }
}
