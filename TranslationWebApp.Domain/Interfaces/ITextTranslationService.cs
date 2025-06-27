using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Domain.Interfaces;

public interface ITextTranslationService
{
    Task<TranslationResult> TranslateAsync(TranslationRequest request);
}
