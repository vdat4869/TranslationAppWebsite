namespace TranslationWebApp.Domain.Models;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Yêu cầu dịch giọng nói: nhận âm thanh, nguồn và đích ngôn ngữ
/// </summary>
public class SpeechTranslationRequest
{
    /// <summary>
    /// Dữ liệu âm thanh (file ghi âm .wav, .mp3...) dưới dạng stream
    /// </summary>
    public Stream AudioData { get; set; } = Stream.Null;

    public IFormFile AudioFile { get; set; } = default!;

    /// <summary>
    /// Ngôn ngữ nói của người dùng (ví dụ: "en-US", "vi-VN")
    /// </summary>
    public string FromLanguage { get; set; } = "en-US";

    /// <summary>
    /// Ngôn ngữ muốn dịch sang (ví dụ: "vi", "ja")
    /// </summary>
    public string ToLanguage { get; set; } = "vi";
}
