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
    public IReadOnlyList<Track> Tracks
    {
        get => _tracks;
        private init => _tracks = new List<Track>();
    }
}