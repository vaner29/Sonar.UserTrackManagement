namespace Sonar.UserTracksManagement.Core.Entities;

public class User
{
    private List<Guid> _friendsIds;
    
    public User(Guid userId, string email, List<Guid> friendsIds)
    {
        UserId = userId;
        Email = email;
        _friendsIds = friendsIds;
    }

    public Guid UserId { get; private set; }
    public string Email { get; private set; }

    public IEnumerable<Guid> Friends => _friendsIds;
}