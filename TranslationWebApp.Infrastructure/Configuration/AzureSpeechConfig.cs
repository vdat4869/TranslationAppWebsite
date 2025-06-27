namespace TranslationWebApp.Infrastructure.Configuration;

/// <summary>
/// Cấu hình cho Azure Speech Service (key, region, endpoint)
/// </summary>
public class AzureSpeechConfig
{
    public string SubscriptionKey { get; set; } = string.Empty;
    public string Region { get; set; } = "eastus";
    public string Endpoint { get; set; } = string.Empty;
}
