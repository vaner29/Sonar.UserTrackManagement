namespace Sonar.UserTracksManagement.Core.Entities;

public class Playlist
{
    private readonly List<PlaylistTrack> _tracks;

    public Playlist(Guid userId, string name)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Name = name;
        _tracks = new List<PlaylistTrack>();
        PlaylistImage = new Image();
        PlaylistMetaDataInfo = new MetaDataInfo(AccessType.Private); // TODO: track access level implementation
    }

    public MetaDataInfo PlaylistMetaDataInfo { get; private init; }
    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public string Name { get; private set; }
    public Image PlaylistImage { get; set; }
        
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