namespace Sonar.UserTracksManagement.Core.Entities;

public class Track
{
    private readonly List<Guid> _userIdWithAccess;
    private Track()
    {
    }

    public Track(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        _userIdWithAccess = new List<Guid>();
    }

    public Guid Id { get; private init; }
    public string Name { get; private set; }

    public IReadOnlyList<Guid> UserIdWithAccess => _userIdWithAccess.AsReadOnly();
}