namespace Sonar.UserTracksManagement.Core.Entities;

public class MetaDataInfo
{
    public MetaDataInfo(AccessType accessType)
    {
        Id = Guid.NewGuid();
        AccessType = accessType;
    }
    
    public Guid Id { get; private set; }
    public AccessType AccessType { get; set; }
}