using Microsoft.CognitiveServices.Speech.Audio;

namespace TranslationWebApp.Infrastructure.Providers;

/// <summary>
/// Cho phép Azure SDK đọc audio từ stream (Memory/File)
/// </summary>
public class BinaryAudioStreamReader : PullAudioInputStreamCallback
{
    private readonly Stream _stream;

    public BinaryAudioStreamReader(Stream stream)
    {
        _stream = stream;
    }

    public override int Read(byte[] dataBuffer, uint size)
    {
        return _stream.Read(dataBuffer, 0, (int)size);
    }

    protected override void Dispose(bool disposing)
    {
        _stream.Dispose();
        base.Dispose(disposing);
    }
}
