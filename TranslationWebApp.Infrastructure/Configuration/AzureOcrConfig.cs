namespace TranslationWebApp.Infrastructure.Configuration;

/// <summary>
/// Cấu hình Azure OCR (Computer Vision)
/// </summary>
public class AzureOcrConfig
{
    public string Endpoint { get; set; } = string.Empty;
    public string SubscriptionKey { get; set; } = string.Empty;
}
