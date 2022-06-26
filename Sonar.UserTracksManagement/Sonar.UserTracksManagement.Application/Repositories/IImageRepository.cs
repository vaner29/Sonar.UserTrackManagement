using Sonar.UserTracksManagement.Core.Entities;

namespace Sonar.UserTracksManagement.Application.Repositories;

public interface IImageRepository
{
    Task SaveImageAsync(Image image, Stream fileStream, CancellationToken cancellationToken);
    Task<Image> GetImageAsync(Guid imageId, CancellationToken cancellationToken);
    Task<Stream> GetImageContentAsync(Image image, CancellationToken cancellationToken);
}