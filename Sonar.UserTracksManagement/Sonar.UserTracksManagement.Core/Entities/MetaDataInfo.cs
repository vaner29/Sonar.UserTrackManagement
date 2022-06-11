namespace Sonar.UserTracksManagement.Core.Entities;

public class MetaDataInfo
{
    public MetaDataInfo(AccessType accessType)
    {
        AccessType = accessType;
    }

    public AccessType AccessType { get; set; }
}