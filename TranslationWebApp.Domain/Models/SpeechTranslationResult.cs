namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Kết quả của quá trình dịch giọng nói
/// </summary>
public class SpeechTranslationResult
{
    /// <summary>
    /// Văn bản gốc được nhận diện từ giọng nói
    /// </summary>
    public string RecognizedText { get; set; } = string.Empty;

    /// <summary>
    /// Văn bản đã dịch sang ngôn ngữ đích
    /// </summary>
    public string TranslatedText { get; set; } = string.Empty;

    /// <summary>
    /// File âm thanh (mp3) chứa bản dịch đã chuyển giọng nói
    /// </summary>
    public byte[]? TranslatedAudio { get; set; } // null nếu chỉ cần text
}
