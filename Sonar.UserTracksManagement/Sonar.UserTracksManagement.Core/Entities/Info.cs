namespace Sonar.UserTracksManagement.Core.Entities;

public class Info
{
    public Info(AccessType accessType)
    {
        AccessType = accessType;
    }

    public AccessType AccessType { get; set; }
}