namespace Sonar.UserTracksManagement.Core.Entities;

public class User
{
    private readonly List<Track> _tracks;
    private User()
    {
    }

    public User(Guid id)
    {
        Id = id;
        _tracks = new List<Track>();
    }

    public Guid Id { get; private init; }
    public IReadOnlyList<Track> Tracks => _tracks.AsReadOnly();

    public void AddTrack(Track track)
    {
        _tracks.Add(track);
    }
}