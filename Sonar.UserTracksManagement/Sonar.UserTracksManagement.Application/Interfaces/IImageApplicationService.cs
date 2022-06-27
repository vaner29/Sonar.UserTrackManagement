using Sonar.UserTracksManagement.Application.Dto;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IImageApplicationService
{
    Task<Guid> SaveImageAsync(
        string token, 
        string path, 
        Stream fileStream, 
        CancellationToken cancellationToken);
    Task SetImageToTrackAsync(
        string token, 
        Guid trackId, 
        Guid imageId, 
        CancellationToken cancellationToken);
    Task SetImageToPlaylistAsync(
        string token, 
        Guid playlistId, 
        Guid imageId, 
        CancellationToken cancellationToken);

    Task<byte[]> GetImageContentByTrackAsync(
        string token,
        Guid trackId,
        CancellationToken cancellationToken);

    Task<byte[]> GetImageContentByPlaylistAsync(
        string token,
        Guid playlistId,
        CancellationToken cancellationToken);
}