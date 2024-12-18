using BSReplayDecoder.Core.Models;

namespace BSReplayDecoder.Core.Handlers;

public class PauseHandler
{
    private readonly BinaryReader _reader;

    public PauseHandler(BinaryReader reader)
    {
        _reader = reader;
    }

    public List<Pause> Decode()
    {
        var length = DecodeInt();
        var pauses = new List<Pause>();
        for (var i = 0; i < length; i++)
        {
            pauses.Add(DecodePause());
        }
        return pauses;
    }

    private Pause DecodePause()
    {
        return new Pause
        {
            Duration = DecodeLong(),
            Time = DecodeFloat()
        };
    }

    private int DecodeInt() => _reader.ReadInt32();
    private float DecodeFloat() => _reader.ReadSingle();
    private long DecodeLong() => _reader.ReadInt64();
}
