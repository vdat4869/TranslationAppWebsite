using TranslationWebApp.Application.Services;
using TranslationWebApp.Application.Providers;
using TranslationWebApp.Domain.Interfaces;
using TranslationWebApp.Infrastructure.Configuration;
using TranslationWebApp.Infrastructure.Providers;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký controller
builder.Services.AddControllers();

// Đăng ký cấu hình Google Translate từ appsettings.json
builder.Services.Configure<AzureTranslatorConfig>(
    builder.Configuration.GetSection("AzureTranslator"));

builder.Services.AddSingleton<ITranslationProvider, AzureTextTranslationProvider>();


// Đăng ký HttpClient để gọi Google Translate API
builder.Services.AddHttpClient<ITranslationProvider, AzureTextTranslationProvider>();

// Đăng ký service nghiệp vụ dịch văn bản
builder.Services.AddScoped<ITextTranslationService, TextTranslationService>();

// Đăng ký provider dịch văn bản
builder.Services.AddScoped<ITranslationProvider, AzureTextTranslationProvider>();

// Đăng ký các provider dịch khác nếu cần (ví dụ: Azure, v.v.)
builder.Services.Configure<AzureSpeechConfig>(
    builder.Configuration.GetSection("AzureSpeech"));

builder.Services.AddScoped<IAudioTranslationProvider, AzureSpeechTranslationProvider>();

builder.Services.AddScoped<ISpeechTranslationService, SpeechTranslationService>();

builder.Services.Configure<AzureOcrConfig>(
    builder.Configuration.GetSection("AzureOcr"));

builder.Services.AddScoped<IImageOcrProvider, AzureImageOcrProvider>();

builder.Services.AddScoped<IImageTranslationService, ImageTranslationService>();

builder.Services.Configure<AzureDocumentTranslationConfig>(
    builder.Configuration.GetSection("AzureDocumentTranslation"));

builder.Services.AddScoped<IDocumentTranslationService, AzureDocumentTranslationProvider>();

var app = builder.Build();

// Cấu hình pipeline HTTP

app.UseStaticFiles(); // 👈 cho phép truy cập file trong wwwroot

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
