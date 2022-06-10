namespace Sonar.UserTracksManagement.Core.Entities;

public class AccessLevel
{
    private AccessType _type;
    private List<Guid> _accessibleUsers;

    public AccessLevel(AccessType type, List<Guid> accessibleUsers)
    {
        _type = type;
        _accessibleUsers = accessibleUsers;
    }
    
    public IReadOnlyCollection<Guid> AccessibleUsers => _accessibleUsers;
}