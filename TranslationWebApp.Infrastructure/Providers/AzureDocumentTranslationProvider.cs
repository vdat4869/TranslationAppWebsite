using Azure;
using Azure.AI.Translation.Document;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Domain.Models;
using TranslationWebApp.Infrastructure.Configuration;
using TranslationWebApp.Domain.Enums;

namespace TranslationWebApp.Infrastructure.Providers;

/// <summary>
/// Provider gọi Azure Document Translation API để dịch tài liệu
/// </summary>
public class AzureDocumentTranslationProvider : IDocumentTranslationService
{
    private readonly AzureDocumentTranslationConfig _config;

    public AzureDocumentTranslationProvider(IOptions<AzureDocumentTranslationConfig> config)
    {
        _config = config.Value;
    }

    public async Task<DocumentTranslationResult> TranslateDocumentAsync(DocumentTranslationRequest request)
    {
        // 1. Upload tài liệu lên Blob Container nguồn
        var blobClient = new BlobContainerClient(_config.BlobStorageConnectionString, _config.BlobSourceContainerName);
        await blobClient.CreateIfNotExistsAsync();

        var blobName = Guid.NewGuid().ToString() + Path.GetExtension(request.FileName);
        var blob = blobClient.GetBlobClient(blobName);

        await blob.UploadAsync(request.DocumentData, overwrite: true);

        // 2. Tạo DocumentTranslationClient
        var credential = new AzureKeyCredential(_config.SubscriptionKey);
        var endpointUri = new Uri(_config.Endpoint);
        var client = new DocumentTranslationClient(endpointUri, credential);

        // 3. Gửi yêu cầu dịch
        var sourceUri = new Uri(_config.SourceContainerUrlWithSas);
        var targetUri = new Uri(_config.TargetContainerUrlWithSas);

        var source = new TranslationSource(sourceUri)
        {
            LanguageCode = request.FromLanguage
        };

        var target = new TranslationTarget(targetUri, request.ToLanguage);

        // Tạo translation input
        var translationInput = new DocumentTranslationInput(source, new[] { target });


        var operation = await client.StartTranslationAsync(translationInput);

        // 4. Chờ cho job dịch hoàn tất
        await operation.WaitForCompletionAsync();

        if (operation.HasCompleted && operation.HasValue)
        {
            // 5. Tạo đường dẫn tạm trả về link download từ container đích
            string translatedBlobUrl = _config.TargetContainerUrlWithSas + "/" + blobName;

            return new DocumentTranslationResult
            {
                DownloadUrl = translatedBlobUrl,
                Status = "Succeeded"
            };
        }

        return new DocumentTranslationResult
        {
            Status = "Failed",
            ErrorMessage = "Dịch tài liệu thất bại"
        };
    }
}
