using BSReplayDecoder.Core.Models;

namespace BSReplayDecoder.Core.Handlers;

public class HeightHandler
{
    private readonly BinaryReader _reader;

    public HeightHandler(BinaryReader reader)
    {
        _reader = reader;
    }

    public List<Height> Decode()
    {
        var length = DecodeInt();
        var heights = new List<Height>();
        for (var i = 0; i < length; i++)
        {
            heights.Add(DecodeHeight());
        }
        return heights;
    }

    private Height DecodeHeight()
    {
        return new Height
        {
            Value = DecodeFloat(),
            Time = DecodeFloat()
        };
    }

    private int DecodeInt() => _reader.ReadInt32();
    private float DecodeFloat() => _reader.ReadSingle();
}
