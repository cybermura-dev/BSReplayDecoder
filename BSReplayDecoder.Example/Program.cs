using BSReplayDecoder.Core;

namespace BSReplayDecoder.Example;

internal static class Program
{
    internal static void Main()
    {
        const string filePath = @"C:\Users\Roman\BSManager\BSInstances\1.29.1\UserData\BeatLeader\ReplayerCache\19791452.bsor";

        try
        {
            // Load replay data from the file
            var replayData = File.ReadAllBytes(filePath);
            
            // Decoding the replay
            var decoder = new ReplayDecoder(replayData);
            var replay = decoder.Decode();

            // Checking the replay information
            Console.WriteLine($"Player Name: {replay.Info.PlayerName}");
            Console.WriteLine($"Player Id: {replay.Info.PlayerId}");
            Console.WriteLine($"Song: {replay.Info.SongName}");
            Console.WriteLine($"Mode: {replay.Info.Mode}");
            Console.WriteLine($"StartTime: {replay.Info.StartTime}");
            Console.WriteLine($"Score: {replay.Info.Score}");
            Console.WriteLine($"Difficulty: {replay.Info.Difficulty}");
            Console.WriteLine($"Number of Frames: {replay.Frames.Count}");
            Console.WriteLine($"Number of Notes: {replay.Notes.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error decoding replay: {ex.Message}");
        }
    }
}