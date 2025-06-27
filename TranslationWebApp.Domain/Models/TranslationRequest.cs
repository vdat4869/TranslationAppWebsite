namespace TranslationWebApp.Domain.Models;

public class TranslationRequest
{
    public string Text { get; set; } = string.Empty;


    public string SourceLanguage { get; set; } = "auto"; // auto-detect
    public string TargetLanguage { get; set; } = "en";
}
