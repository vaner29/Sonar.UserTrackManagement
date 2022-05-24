namespace Sonar.UserTracksManagement.Core.Entities;

public class Playlist
{
    private readonly List<PlaylistTrack> _tracks;
    private Playlist()
    {
    }

    public Playlist(Guid userId, string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        _tracks = new List<PlaylistTrack>();
    }

    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public string Name { get; private set; }
    public IReadOnlyList<PlaylistTrack> Tracks
    { 
        get => _tracks;
        private init => _tracks = new List<PlaylistTrack>();
    }

}