namespace BSReplayDecoder.Core.Models;

public struct ReplayInfo
{
    public string Version { get; set; }
    public string GameVersion { get; set; }
    public string Timestamp { get; set; }
    public string PlayerId { get; set; }
    public string PlayerName { get; set; }
    public string Platform { get; set; }
    public string TrackingSystem { get; set; }
    public string Hmd { get; set; }
    public string Controller { get; set; }
    public string Hash { get; set; }
    public string SongName { get; set; }
    public string Mapper { get; set; }
    public string Difficulty { get; set; }
    public int Score { get; set; }
    public string Mode { get; set; }
    public string Environment { get; set; }
    public string Modifiers { get; set; }
    public float JumpDistance { get; set; }
    public bool LeftHanded { get; set; }
    public float Height { get; set; }
    public float StartTime { get; set; }
    public float FailTime { get; set; }
    public float Speed { get; set; }
}