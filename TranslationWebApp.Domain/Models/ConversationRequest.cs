using Microsoft.AspNetCore.Http;

namespace TranslationWebApp.Domain.Models;

/// <summary>
/// Request dịch trong chế độ hội thoại song ngữ
/// </summary>
public class ConversationRequest
{
    public IFormFile AudioFile { get; set; } = default!;
    public string Speaker { get; set; } = "A"; // Có thể là "A" hoặc "B"
    public string FromLanguage { get; set; } = "en-US";
    public string ToLanguage { get; set; } = "vi";
}
