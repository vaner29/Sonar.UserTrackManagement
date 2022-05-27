namespace Sonar.UserTracksManagement.Core.Entities;

public class Playlist
{
    private readonly List<PlaylistTrack> _tracks;

    public Playlist(User user, string name)
    {
        Id = Guid.NewGuid();
        
        User = user;
        Name = name;
        _tracks = new List<PlaylistTrack>();
    }

    public Guid Id { get; private init; }
    public User User { get; private init; }
    public string Name { get; private set; }
    public IReadOnlyList<PlaylistTrack> Tracks => _tracks.AsReadOnly();

    public void AddTrack(PlaylistTrack track)
    {
        _tracks.Add(track);
    }

    public void RemoveTrack(PlaylistTrack track)
    {
        _tracks.Remove(track);
    }

}