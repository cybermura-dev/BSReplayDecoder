using BSReplayDecoder.Core.Models;

namespace BSReplayDecoder.Core.Handlers;

public class FrameHandler
{
    private readonly BinaryReader _reader;

    public FrameHandler(BinaryReader reader)
    {
        _reader = reader;
    }

    public List<Frame> Decode()
    {
        var length = DecodeInt();
        var frames = new List<Frame>();
        for (var i = 0; i < length; i++)
        {
            frames.Add(DecodeFrame());
        }
        return frames;
    }

    private int DecodeInt() => _reader.ReadInt32();
    private float DecodeFloat() => _reader.ReadSingle();

    private Frame DecodeFrame()
    {
        return new Frame
        {
            Time = DecodeFloat(),
            Fps = DecodeInt(),
            Head = DecodeEuler(),
            Left = DecodeEuler(),
            Right = DecodeEuler()
        };
    }

    private Euler DecodeEuler()
    {
        return new Euler
        {
            Position = DecodeVector3(),
            Rotation = DecodeQuaternion()
        };
    }

    private Vector3 DecodeVector3()
    {
        return new Vector3
        {
            X = DecodeFloat(),
            Y = DecodeFloat(),
            Z = DecodeFloat()
        };
    }

    private Quaternion DecodeQuaternion()
    {
        return new Quaternion
        {
            X = DecodeFloat(),
            Y = DecodeFloat(),
            Z = DecodeFloat(),
            W = DecodeFloat()
        };
    }
}
