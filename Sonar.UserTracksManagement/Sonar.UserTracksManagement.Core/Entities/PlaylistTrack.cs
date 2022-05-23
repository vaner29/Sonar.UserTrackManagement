namespace Sonar.UserTracksManagement.Core.Entities;

public class PlaylistTrack
{
    private PlaylistTrack()
    {
    }

    public PlaylistTrack(uint number, Track track)
    {
        Id = Guid.NewGuid();
        Number = number;
        Track = track;
    }

    public Guid Id { get; private set; }
    public uint Number { get; private set; }
    public Track Track { get; private set; }
}