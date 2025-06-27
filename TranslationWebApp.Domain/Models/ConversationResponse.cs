namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Phản hồi sau khi xử lý 1 lượt hội thoại
/// </summary>
public class ConversationResponse
{
    public string Speaker { get; set; } = string.Empty;
    public string RecognizedText { get; set; } = string.Empty;
    public string TranslatedText { get; set; } = string.Empty;
    public byte[]? TranslatedAudio { get; set; } // dùng base64 nếu cần
}
