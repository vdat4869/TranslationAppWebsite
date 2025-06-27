using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Options;
using System.Text;
using TranslationWebApp.Application.Providers;
using TranslationWebApp.Domain.Models;
using TranslationWebApp.Infrastructure.Configuration;

namespace TranslationWebApp.Infrastructure.Providers;

/// <summary>
/// Provider thực hiện dịch giọng nói qua Azure Speech + Translator
/// </summary>
public class AzureSpeechTranslationProvider : IAudioTranslationProvider
{
    private readonly AzureSpeechConfig _config;

    public AzureSpeechTranslationProvider(IOptions<AzureSpeechConfig> config)
    {
        _config = config.Value;
    }

    /// <summary>
    /// Dịch từ giọng nói → văn bản → bản dịch → đọc lại thành giọng nói
    /// </summary>
    public async Task<SpeechTranslationResult> TranslateFromSpeechAsync(SpeechTranslationRequest request)
    {
        // 1. Tạo cấu hình Azure Speech
        var speechConfig = SpeechConfig.FromSubscription(_config.SubscriptionKey, _config.Region);
        speechConfig.SpeechRecognitionLanguage = request.FromLanguage;

        // 2. Chuẩn bị audio stream từ file gửi vào
        using var audioInput = AudioConfig.FromStreamInput(new BinaryAudioStreamReader(request.AudioData));

        // 3. Nhận diện văn bản từ giọng nói
        using var recognizer = new SpeechRecognizer(speechConfig, audioInput);
        var recognition = await recognizer.RecognizeOnceAsync();

        if (recognition.Reason != ResultReason.RecognizedSpeech)
            throw new InvalidOperationException("Không nhận diện được giọng nói.");

        var recognizedText = recognition.Text;

        // 4. Dịch văn bản đó sang ngôn ngữ đích (dùng Azure Translator hoặc Google tùy sau)
        var translatedText = await TranslateTextAsync(recognizedText, request.FromLanguage, request.ToLanguage);

        // 5. Chuyển bản dịch thành giọng nói
        var audioBytes = await TextToSpeechAsync(translatedText, request.ToLanguage);

        return new SpeechTranslationResult
        {
            RecognizedText = recognizedText,
            TranslatedText = translatedText,
            TranslatedAudio = audioBytes
        };
    }

    /// <summary>
    /// Gọi dịch văn bản bằng Azure Translator (bước này tạm fake)
    /// </summary>
    private Task<string> TranslateTextAsync(string text, string fromLang, string toLang)
    {
        // TODO: sau này thay bằng Azure Translator hoặc gọi GoogleTranslationProvider lại
        return Task.FromResult($"[Giả lập dịch sang {toLang}]: {text}");
    }

    /// <summary>
    /// Chuyển văn bản thành giọng nói (TTS)
    /// </summary>
    private async Task<byte[]> TextToSpeechAsync(string text, string languageCode)
    {
        var config = SpeechConfig.FromSubscription(_config.SubscriptionKey, _config.Region);
        config.SpeechSynthesisLanguage = languageCode;
        config.SpeechSynthesisVoiceName = GetVoiceByLanguage(languageCode);

        // 1. Tạo MemoryStream để chứa âm thanh
        var memoryStream = new MemoryStream();

        // 2. Bọc MemoryStream thành AudioOutputStream
        var pushStream = AudioOutputStream.CreatePushStream(new MemoryStreamAudioOutput(memoryStream));
        var audioOutput = AudioConfig.FromStreamOutput(pushStream);

        // 3. Tạo SpeechSynthesizer
        using var synthesizer = new SpeechSynthesizer(config, audioOutput);

        // 4. Gọi TTS
        var result = await synthesizer.SpeakTextAsync(text);

        if (result.Reason != ResultReason.SynthesizingAudioCompleted)
            throw new InvalidOperationException("Không tạo được giọng nói từ văn bản.");

        // 5. Reset vị trí đọc và trả kết quả
        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream.ToArray();
    }

    /// <summary>
    /// Trả về tên voice mặc định theo ngôn ngữ
    /// </summary>
    private string GetVoiceByLanguage(string langCode)
    {
        return langCode switch
        {
            "vi" => "vi-VN-HoaiMyNeural",
            "en" => "en-US-JennyNeural",
            _ => "en-US-JennyNeural"
        };
    }
}
