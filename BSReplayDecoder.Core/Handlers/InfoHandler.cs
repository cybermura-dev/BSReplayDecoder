using BSReplayDecoder.Core.Models;

namespace BSReplayDecoder.Core.Handlers;

public class InfoHandler
{
    private readonly BinaryReader _reader;

    public InfoHandler(BinaryReader reader)
    {
        _reader = reader;
    }

    public ReplayInfo Decode()
    {
        return new ReplayInfo
        {
            Version = DecodeString(),
            GameVersion = DecodeString(),
            Timestamp = DecodeString(),
            PlayerId = DecodeString(),
            PlayerName = DecodeString(),
            Platform = DecodeString(),
            TrackingSystem = DecodeString(),
            Hmd = DecodeString(),
            Controller = DecodeString(),
            Hash = DecodeString(),
            SongName = DecodeString(),
            Mapper = DecodeString(),
            Difficulty = DecodeString(),
            Score = DecodeInt(),
            Mode = DecodeString(),
            Environment = DecodeString(),
            Modifiers = DecodeString(),
            JumpDistance = DecodeFloat(),
            LeftHanded = DecodeBool(),
            Height = DecodeFloat(),
            StartTime = DecodeFloat(),
            FailTime = DecodeFloat(),
            Speed = DecodeFloat()
        };
    }

    private int DecodeInt() => _reader.ReadInt32();
    private byte DecodeByte() => _reader.ReadByte();
    private float DecodeFloat() => _reader.ReadSingle();
    private bool DecodeBool() => _reader.ReadByte() != 0;
    private string DecodeString()
    {
        var length = DecodeInt();
        return new string(_reader.ReadChars(length));
    }
}