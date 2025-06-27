namespace TranslationWebApp.Infrastructure.Configuration;

public class AzureTranslatorConfig
{
    public string Endpoint { get; set; } = string.Empty;
    public string SubscriptionKey { get; set; } = string.Empty;
    public string Region { get; set; } = "southeastasia"; // hoặc region của mày
}
