using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using TranslationWebApp.Application.Providers;
using TranslationWebApp.Domain.Models;
using TranslationWebApp.Infrastructure.Configuration;

namespace TranslationWebApp.Infrastructure.Providers;

/// <summary>
/// Provider dịch văn bản bằng Azure Translator
/// </summary>
public class AzureTextTranslationProvider : ITranslationProvider
{
    private readonly AzureTranslatorConfig _config;
    private readonly HttpClient _httpClient;

    public AzureTextTranslationProvider(HttpClient httpClient, IOptions<AzureTranslatorConfig> config)
    {
        _httpClient = httpClient;
        _config = config.Value;
    }


    public async Task<TranslationResult> TranslateAsync(TranslationRequest request)
    {
        // 1. Tạo URL gọi API
        string route = request.SourceLanguage == "auto"
    ? $"/translate?api-version=3.0&to={request.TargetLanguage}"
    : $"/translate?api-version=3.0&from={request.SourceLanguage}&to={request.TargetLanguage}";

        string uri = _config.Endpoint.TrimEnd('/') + route;

        // 2. Tạo nội dung JSON gửi lên
        var requestBody = new object[] { new { Text = request.Text } };
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // 3. Thêm header xác thực
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.SubscriptionKey);
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", _config.Region);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // 4. Gửi yêu cầu dịch
        var response = await _httpClient.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);

        var translatedText = doc.RootElement[0].GetProperty("translations")[0].GetProperty("text").GetString();

        return new TranslationResult
        {
            TranslatedText = translatedText ?? ""
        };
    }
}
