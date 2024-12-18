using System.Reflection;
using BSReplayDecoder.Core;
using BSReplayDecoder.Core.Models;
using BSReplayDecoder.Core.Types;

namespace BSReplayDecoder.Tests;

[TestFixture]
public class ReplayDecoderTests
{
    private byte[] _validReplayData;
    private byte[] _invalidReplayData;
    
    [SetUp]
    public void SetUp()
    {
        // Retrieve the replay as a stream from an embedded resource
        _validReplayData = GetEmbeddedResource("BSReplayDecoder.Tests.Resources.Bluenation_replay.bsor");
        
        // Retrieve an invalid replay (example: file not found or corrupted)
        _invalidReplayData = GetEmbeddedResource("BSReplayDecoder.Tests.Resources.Invalid_replay.bsor");
    }
    
    [Test]
    public void Decode_ValidReplay_ShouldReturnValidReplayObject()
    {
        // Arrange
        var decoder = new ReplayDecoder(_validReplayData);

        // Act
        var replay = decoder.Decode();

        // Assert
        Assert.Multiple(() =>
        {
            // Check the main data of the replay
            Assert.That(replay.Info.PlayerName, Is.EqualTo("Kip")); // Checking player name
            Assert.That(replay.Info.PlayerId, Is.EqualTo("76561198427751585")); // Checking player id
            Assert.That(replay.Info.SongName, Is.EqualTo("Bluenation")); // Checking song name
            Assert.That(replay.Info.Score, Is.EqualTo(2260739)); // Checking map score
            Assert.That(replay.Info.Mode, Is.EqualTo("Standard")); // Checking replay mode
            Assert.That(replay.Info.StartTime, Is.EqualTo(0)); // Check that the start time is greater than 0

            // Check for frames
            Assert.That(replay.Frames, Is.Not.Empty); // Check if frames are more than 0
            Assert.That(replay.Frames[0].Time, Is.GreaterThan(0)); // Check that the time of the first frame is greater than 0
            Assert.That(replay.Frames[0].Fps, Is.GreaterThan(0)); // Check that the FPS of the first frame is greater than 0

            // Check if there are notes
            Assert.That(replay.Notes, Is.Not.Empty); // Check that the notes are greater than 0
            Assert.That(replay.Notes[0].NoteId, Is.GreaterThan(0)); // Check that the ID of the first note is greater than 0
            Assert.That(replay.Notes[0].EventTime,
                Is.GreaterThan(0)); // Check that the event time of the first note is greater than 0
        });
    }
    
    [Test]
    public void Decode_ValidReplay_ShouldHaveCorrectFrames()
    {
        // Arrange
        var decoder = new ReplayDecoder(_validReplayData);

        // Act
        var replay = decoder.Decode();

        // Assert
        Assert.That(replay.Frames, Has.Count.GreaterThan(1));  // Checking that the number of frames is greater than 1
        Assert.That(replay.Frames[1].Time, Is.GreaterThan(replay.Frames[0].Time));  // Checking that the time of the second frame is greater than the time of the first frame
    }
    
    [Test]
    public void Decode_ValidReplay_ShouldContainCorrectNoteEvents()
    {
        // Arrange
        var decoder = new ReplayDecoder(_validReplayData);

        // Act
        var replay = decoder.Decode();

        // Assert
        Assert.That(replay.Notes, Has.Some.Property(nameof(Note.EventType)).EqualTo((int)NoteEventType.Good));  // Checking for the presence of good notes
        Assert.That(replay.Notes, Has.Some.Property(nameof(Note.EventType)).EqualTo((int)NoteEventType.Bad));   // Checking for bad notes
    }
    
    [Test]
    public void Decode_InvalidReplay_ShouldThrowException()
    {
        // Arrange
        var decoder = new ReplayDecoder(_invalidReplayData);

        // Act & Assert
        Assert.Throws<EndOfStreamException>(() => decoder.Decode(), "Unable to read beyond the end of the stream.");
    }
    
    [Test]
    public void Decode_IncompleteReplayData_ShouldHandleGracefully()
    {
        // Arrange: Incomplete or corrupted replay data
        var incompleteReplayData = new byte[] { 1, 2, 3, 4 };  // Just an example of corrupted data
        var decoder = new ReplayDecoder(incompleteReplayData);

        // Act & Assert
        Assert.Throws<EndOfStreamException>(() => decoder.Decode(), "Expected an exception for incomplete or corrupted replay data.");
    }
    
    private static byte[] GetEmbeddedResource(string resourceName)
    {
        // Getting the current build
        var assembly = Assembly.GetExecutingAssembly();

        // Getting the data stream from the resource
        using var stream = assembly.GetManifestResourceStream(resourceName);

        // Read the entire stream into a byte array
        using var memoryStream = new MemoryStream();
        
        stream?.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}