namespace BSReplayDecoder.Core.Models;

public struct CutInfo
{
    public bool SpeedOk { get; set; }
    public bool DirectionOk { get; set; }
    public bool SaberTypeOk { get; set; }
    public bool WasCutTooSoon { get; set; }
    public float SaberSpeed { get; set; }
    public Vector3 SaberDir { get; set; }
    public int SaberType { get; set; }
    public float TimeDeviation { get; set; }
    public float CutDirDeviation { get; set; }
    public Vector3 CutPoint { get; set; }
    public Vector3 CutNormal { get; set; }
    public float CutDistanceToCenter { get; set; }
    public float CutAngle { get; set; }
    public float BeforeCutRating { get; set; }
    public float AfterCutRating { get; set; }
}