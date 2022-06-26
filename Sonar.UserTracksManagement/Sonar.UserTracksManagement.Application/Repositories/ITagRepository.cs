using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Repositories;

public interface ITagRepository
{
    Task<Tag> AddAsync(string tagName, CancellationToken cancellationToken);
    Task<Tag> GetAsync(string tagName, CancellationToken cancellationToken);
    Task Assign(MetaDataInfo metaDataInfo, Tag tag, CancellationToken cancellationToken);
    Task Remove(MetaDataInfo metaDataInfo, Tag tag, CancellationToken cancellationToken);
}