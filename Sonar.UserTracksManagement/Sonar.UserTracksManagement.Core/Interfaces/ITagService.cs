using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Core.Interfaces;

public interface ITagService
{
    void AssignTagToMetaDataInfo(MetaDataInfo metaDataInfo, Tag tag);
    void RemoveTagFromMetaDataInfo(MetaDataInfo metaDataInfo, Tag tag);
}