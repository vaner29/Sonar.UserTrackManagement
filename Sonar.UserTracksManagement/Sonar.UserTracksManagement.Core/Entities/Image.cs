namespace Sonar.UserTracksManagement.Core.Entities;

public class Image
{
    public Image()
    {
        Id = Guid.NewGuid();
        Path = "";
    }
    
    public Image(string name, string path)
    {
        Id = Guid.NewGuid();
        Path = path;
    }
    
    public string Path { get; private init; }
    public Guid Id { get; private init; }
}