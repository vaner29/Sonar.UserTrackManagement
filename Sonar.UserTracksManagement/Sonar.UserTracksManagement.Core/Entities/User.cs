namespace Sonar.UserTracksManagement.Core.Entities;

public class User
{
    private User()
    {
        Tracks = new List<Track>();
    }

    public User(Guid id)
    {
        Id = id;
        Tracks = new List<Track>();
    }

    public Guid Id { get; private set; }
    public List<Track> Tracks { get; private set; }
}