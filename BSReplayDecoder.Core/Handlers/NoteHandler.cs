using BSReplayDecoder.Core.Models;
using BSReplayDecoder.Core.Types;

namespace BSReplayDecoder.Core.Handlers;

public class NoteHandler
{
    private readonly BinaryReader _reader;

    public NoteHandler(BinaryReader reader)
    {
        _reader = reader;
    }

    public List<Note> Decode()
    {
        var length = DecodeInt();
        var notes = new List<Note>();
        for (var i = 0; i < length; i++)
        {
            notes.Add(DecodeNote());
        }
        return notes;
    }

    private Note DecodeNote()
    {
        var note = new Note
        {
            NoteId = DecodeInt(),
            EventTime = DecodeFloat(),
            SpawnTime = DecodeFloat(),
            EventType = DecodeInt()
        };

        if (note.EventType is (int)NoteEventType.Good or (int)NoteEventType.Bad)
        {
            note.NoteCutInfo = DecodeCutInfo();
        }

        return note;
    }

    private CutInfo DecodeCutInfo()
    {
        return new CutInfo
        {
            SpeedOk = DecodeBool(),
            DirectionOk = DecodeBool(),
            SaberTypeOk = DecodeBool(),
            WasCutTooSoon = DecodeBool(),
            SaberSpeed = DecodeFloat(),
            SaberDir = DecodeVector3(),
            SaberType = DecodeInt(),
            TimeDeviation = DecodeFloat(),
            CutDirDeviation = DecodeFloat(),
            CutPoint = DecodeVector3(),
            CutNormal = DecodeVector3(),
            CutDistanceToCenter = DecodeFloat(),
            CutAngle = DecodeFloat(),
            BeforeCutRating = DecodeFloat(),
            AfterCutRating = DecodeFloat()
        };
    }

    private int DecodeInt() => _reader.ReadInt32();
    private float DecodeFloat() => _reader.ReadSingle();
    private bool DecodeBool() => _reader.ReadByte() != 0;
    private Vector3 DecodeVector3()
    {
        return new Vector3
        {
            X = DecodeFloat(),
            Y = DecodeFloat(),
            Z = DecodeFloat()
        };
    }
}
