namespace Sonar.UserTracksManagement.Core.Entities;

public class Playlist
{
    private Playlist()
    {
        Tracks = new List<PlaylistTrack>();
    }

    public Playlist(Guid userId, string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        Tracks = new List<PlaylistTrack>();
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public List<PlaylistTrack> Tracks { get; private set; }

}