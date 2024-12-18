namespace BSReplayDecoder.Core.Models;

public struct Frame
{
    public float Time { get; set; }
    public int Fps { get; set; }
    public Euler Head { get; set; }
    public Euler Left { get; set; }
    public Euler Right { get; set; }
}