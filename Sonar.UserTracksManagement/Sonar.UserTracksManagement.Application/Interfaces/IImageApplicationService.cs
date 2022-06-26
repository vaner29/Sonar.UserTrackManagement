using Sonar.UserTracksManagement.Application.Dto;

namespace Sonar.UserTracksManagement.Application.Interfaces;

public interface IImageApplicationService
{
    Task<Guid> SaveImage(string token, string path, Stream fileStream);
    Task SetImageToTrackAsync(string token, Guid trackId, ImageFormDto imageDto);
    Task SetImageToPlaylistAsync(string token, Guid playlistId, ImageFormDto playlistDto);
}