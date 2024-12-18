namespace BSReplayDecoder.Core.Models;

public struct Note
{
    public int NoteId { get; set; }
    public float EventTime { get; set; }
    public float SpawnTime { get; set; }
    public int EventType { get; set; }
    public CutInfo NoteCutInfo { get; set; }
}