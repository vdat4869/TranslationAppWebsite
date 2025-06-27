namespace TranslationWebApp.Application.Providers;

/// <summary>
/// Giao diện chuyển giọng nói thành văn bản (Speech ➜ Text)
/// </summary>
public interface ISpeechRecognitionProvider
{
    Task<string> RecognizeSpeechAsync(Stream audioStream, string fromLanguage);
}
