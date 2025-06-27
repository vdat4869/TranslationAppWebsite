using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;
using TranslationWebApp.Application.Providers;

namespace TranslationWebApp.Application.Services;

/// <summary>
/// Service xử lý hội thoại song ngữ: nhận giọng nói, dịch và trả lại kết quả
/// </summary>
public class ConversationService : IConversationService
{
    private readonly ISpeechRecognitionProvider _speechRecognizer;
    private readonly ITranslationProvider _textTranslator;
    private readonly ITextToSpeechProvider _textToSpeech;

    public ConversationService(
        ISpeechRecognitionProvider speechRecognizer,
        ITranslationProvider textTranslator,
        ITextToSpeechProvider textToSpeech)
    {
        _speechRecognizer = speechRecognizer;
        _textTranslator = textTranslator;
        _textToSpeech = textToSpeech;
    }

    /// <summary>
    /// Xử lý 1 lượt hội thoại: từ audio → text → dịch → phát lại
    /// </summary>
    public async Task<ConversationResponse> HandleConversationAsync(ConversationRequest request)
    {
        // 1. Đọc dữ liệu âm thanh từ file được gửi lên
        using var stream = request.AudioFile.OpenReadStream();

        // 2. Nhận diện giọng nói (audio → văn bản gốc)
        var recognizedText = await _speechRecognizer.RecognizeSpeechAsync(stream, request.FromLanguage);

        // 3. Tạo request dịch văn bản
        var translationRequest = new TranslationRequest
        {
            Text = recognizedText,
            SourceLanguage = request.FromLanguage,
            TargetLanguage = request.ToLanguage
        };

        var translationResult = await _textTranslator.TranslateAsync(translationRequest);
        var translatedText = translationResult.TranslatedText;



        // 4. Chuyển văn bản dịch thành âm thanh (Text-to-Speech)
        var translatedAudio = await _textToSpeech.SynthesizeSpeechAsync(translatedText, request.ToLanguage);

        // 5. Trả về kết quả gồm người nói, văn bản gốc, bản dịch và audio đã dịch
        return new ConversationResponse
        {
            Speaker = request.Speaker,
            RecognizedText = recognizedText,
            TranslatedText = translatedText,
            TranslatedAudio = translatedAudio
        };
    }
}
