namespace Sonar.UserTracksManagement.Core.Entities;

public class MetaDataInfo
{
    private readonly List<Tag> _tags;
    public MetaDataInfo(AccessType accessType)
    {
        Id = Guid.NewGuid();
        _tags = new List<Tag>();
        AccessType = accessType;
    }
    
    public Guid Id { get; private set; }
    public AccessType AccessType { get; set; }
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    public void AddTag(Tag tag)
    {
        _tags.Add(tag);
    }
    
    public void RemoveTag(Tag tag)
    {
        _tags.Remove(tag);
    }
}