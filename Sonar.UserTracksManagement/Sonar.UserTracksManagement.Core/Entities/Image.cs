namespace Sonar.UserTracksManagement.Core.Entities;

public class Image
{
    public Image()
    {
        Id = Guid.NewGuid();
        Path = "";
    }
    
    public Image(string path, Guid guid)
    {
        Id = Guid.NewGuid();
        Path = path;
        OwnerId = guid;
    }
    
    public string Path { get; private init; }
    public Guid Id { get; private init; }
    public Guid OwnerId { get; private init; }
}