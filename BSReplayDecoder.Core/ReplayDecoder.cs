using BSReplayDecoder.Core.Handlers;
using BSReplayDecoder.Core.Models;
using BSReplayDecoder.Core.Types;

namespace BSReplayDecoder.Core;

public class ReplayDecoder
{
    private readonly BinaryReader _reader;

    public ReplayDecoder(byte[] arrayBuffer)
    {
        _reader = new BinaryReader(new MemoryStream(arrayBuffer));
    }

    public Replay Decode()
    {
        var magic = DecodeInt();
        var version = DecodeByte();

        if (version == 1 && magic == 0x442d3d69)
        {
            var replay = new Replay();

            for (var a = 0; a <= (int)StructType.Pauses; a++)
            {
                var type = DecodeByte();
                switch (type)
                {
                    case (byte)StructType.Info:
                        replay.Info = new InfoHandler(_reader).Decode();
                        break;
                    case (byte)StructType.Frames:
                        replay.Frames = new FrameHandler(_reader).Decode();
                        break;
                    case (byte)StructType.Notes:
                        replay.Notes = new NoteHandler(_reader).Decode();
                        break;
                    case (byte)StructType.Walls:
                        replay.Walls = new WallHandler(_reader).Decode();
                        break;
                    case (byte)StructType.Heights:
                        replay.Heights = new HeightHandler(_reader).Decode();
                        break;
                    case (byte)StructType.Pauses:
                        replay.Pauses = new PauseHandler(_reader).Decode();
                        break;
                }
            }

            return replay;
        }
        else
        {
            throw new Exception("Error: failed to decode replay");
        }
    }

    private int DecodeInt() => _reader.ReadInt32();
    private byte DecodeByte() => _reader.ReadByte();
    private float DecodeFloat() => _reader.ReadSingle();
    private string DecodeString() => new string(_reader.ReadChars(DecodeInt()));
}
