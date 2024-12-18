namespace BSReplayDecoder.Core.Models;

public struct Replay
{
    public ReplayInfo Info { get; set; }
    public List<Frame> Frames { get; set; }
    public List<Note> Notes { get; set; }
    public List<Wall> Walls { get; set; }
    public List<Height> Heights { get; set; }
    public List<Pause> Pauses { get; set; }
}