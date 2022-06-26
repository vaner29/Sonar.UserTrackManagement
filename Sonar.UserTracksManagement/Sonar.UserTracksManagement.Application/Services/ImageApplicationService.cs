using Sonar.UserTracksManagement.Application.Dto;
using Sonar.UserTracksManagement.Application.Interfaces;

namespace Sonar.UserTracksManagement.Application.Services;

public class ImageApplicationService : IImageApplicationService
{
    public Task<Guid> SaveImage(string token, string path, Stream fileStream)
    {
        throw new NotImplementedException();
    }

    public Task SetImageToTrackAsync(string token, Guid trackId, ImageFormDto imageDto)
    {
        throw new NotImplementedException();
    }

    public Task SetImageToPlaylistAsync(string token, Guid playlistId, ImageFormDto playlistDto)
    {
        throw new NotImplementedException();
    }
}