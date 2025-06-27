namespace TranslationWebApp.Application.Providers;

/// <summary>
/// Giao diện chuyển văn bản thành giọng nói (Text ➜ Audio)
/// </summary>
public interface ITextToSpeechProvider
{
    Task<byte[]> SynthesizeSpeechAsync(string text, string language);
}
