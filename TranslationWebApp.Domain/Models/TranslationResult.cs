namespace TranslationWebApp.Domain.Models;

public class TranslationResult
{
    public string TranslatedText { get; set; } = string.Empty;
    public string DetectedSourceLanguage { get; set; } = string.Empty;
}
