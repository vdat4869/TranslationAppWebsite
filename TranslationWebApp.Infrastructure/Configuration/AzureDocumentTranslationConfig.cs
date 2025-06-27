namespace TranslationWebApp.Infrastructure.Configuration;

/// <summary>
/// Cấu hình Azure Document Translation API + Blob Storage
/// </summary>
public class AzureDocumentTranslationConfig
{
    public string Endpoint { get; set; } = string.Empty;
    public string SubscriptionKey { get; set; } = string.Empty;

    public string SourceContainerUrlWithSas { get; set; } = string.Empty;
    public string TargetContainerUrlWithSas { get; set; } = string.Empty;

    public string BlobStorageConnectionString { get; set; } = string.Empty;
    public string BlobSourceContainerName { get; set; } = "documents-to-translate";
}
