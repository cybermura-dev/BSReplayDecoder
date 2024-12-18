using BSReplayDecoder.Core.Models;

namespace BSReplayDecoder.Core.Handlers;

public class WallHandler
{
    private readonly BinaryReader _reader;

    public WallHandler(BinaryReader reader)
    {
        _reader = reader;
    }

    public List<Wall> Decode()
    {
        var length = DecodeInt();
        var walls = new List<Wall>();
        for (var i = 0; i < length; i++)
        {
            walls.Add(DecodeWall());
        }
        return walls;
    }

    private Wall DecodeWall()
    {
        return new Wall
        {
            WallId = DecodeInt(),
            Energy = DecodeFloat(),
            Time = DecodeFloat(),
            SpawnTime = DecodeFloat()
        };
    }

    private int DecodeInt() => _reader.ReadInt32();
    private float DecodeFloat() => _reader.ReadSingle();
}
