using Sonar.UserTracksManagement.Core.Entities;
using Sonar.UserTracksManagement.Core.Interfaces;

namespace Sonar.UserTracksManagement.Core.Services;

public class TagService : ITagService
{
    public void AssignTagToMetaDataInfo(MetaDataInfo metaDataInfo, Tag tag)
    {
        metaDataInfo.AddTag(tag);
    }

    public void RemoveTagFromMetaDataInfo(MetaDataInfo metaDataInfo, Tag tag)
    {
        metaDataInfo.RemoveTag(tag);
    }
}