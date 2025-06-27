using TranslationWebApp.Domain.Models;

namespace TranslationWebApp.Domain.Interfaces;

public interface IConversationService
{
    Task<ConversationResponse> HandleConversationAsync(ConversationRequest request);
}
