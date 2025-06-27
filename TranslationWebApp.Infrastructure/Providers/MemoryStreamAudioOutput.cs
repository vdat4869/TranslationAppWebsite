using Microsoft.CognitiveServices.Speech.Audio;

namespace TranslationWebApp.Infrastructure.Providers;

/// <summary>
/// Lớp bọc MemoryStream để ghi âm thanh từ Azure TTS
/// </summary>
public class MemoryStreamAudioOutput : PushAudioOutputStreamCallback
{
    private readonly MemoryStream _memoryStream;

    public MemoryStreamAudioOutput(MemoryStream memoryStream)
    {
        _memoryStream = memoryStream;
    }

    public override uint Write(byte[] dataBuffer)
    {
        _memoryStream.Write(dataBuffer, 0, dataBuffer.Length);
        return (uint)dataBuffer.Length;
    }

    protected override void Dispose(bool disposing)
    {
        _memoryStream.Flush();
        base.Dispose(disposing);
    }
}
